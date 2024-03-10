using System.Threading;
using Cysharp.Threading.Tasks;
using Data.Interfaces;
using Interfaces;
using Movement;
using Movement.Interfaces;
using Pool.Interfaces;
using UnityEngine;

namespace Player
{
	public class Bullet : MonoBehaviour, IPoolObject
	{
		private IMovement _movement;
		private IPoolManager<Bullet> _pool;
		private CancellationTokenSource _returnToPoolCancellationToken;

		private bool _isHit; //safety if two destructible object overlaps. Then Destroy only one

		private IBulletData _bulletData;
		
		public void Init(Vector2 startPosition, Vector2 direction, IPoolManager<Bullet> pool, IBulletData bulletData)
		{
			_pool = pool;
			_bulletData = bulletData;
			transform.position = startPosition;
			SetMovement(direction);
			CountLifeTime().Forget();
		}

		private void FixedUpdate()
		{
			if (_movement != null)
			{
				_movement.Move();
			}
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			TryHit(other.gameObject);
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			TryHit(other.gameObject);
		}

		private void TryHit(GameObject other)
		{
			if (!_isHit && other.gameObject.TryGetComponent<IDestructible>(out var hitObject))
			{
				Hit(hitObject);
			}
		}
	
		private void Hit(IDestructible other)
		{
			_isHit = true;
			ReturnToPool();
			other.Destroyed();
		}

		private async UniTaskVoid CountLifeTime()
		{
			_returnToPoolCancellationToken = new CancellationTokenSource();
			await UniTask.Delay(_bulletData.BulletLifeTimeInMS, cancellationToken: _returnToPoolCancellationToken.Token);
			ReturnToPool();
		}
	
		private void SetMovement(Vector2 direction)
		{
			_movement = new SimpleMovement();
			_movement.Init(transform, direction, _bulletData.BulletSpeed);
		}

		private void ReturnToPool()
		{
			_returnToPoolCancellationToken?.Cancel();
			_pool.ReleaseObject(this);
		}
	
		public void AfterGet()
		{
			_isHit = false;
			gameObject.SetActive(true);
		}

		public void BeforeRelease()
		{
			gameObject.SetActive(false);
		}
	}
}