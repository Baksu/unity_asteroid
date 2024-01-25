using System.Threading;
using Cysharp.Threading.Tasks;
using Interfaces;
using Movement;
using Movement.Interfaces;
using Pool.Interfaces;
using UnityEngine;

namespace Player
{
	public class Bullet : MonoBehaviour, IPoolObject
	{
		//TODO: it should be moved to scriptable object as a data and pass in Init func
		public float bulletSpeed = 50f;
		public int bulletLifeTimeInMS;
		
		private IMovement _movement;
		private IPoolManager<Bullet> _pool;
		private CancellationTokenSource _returnToPoolCancellationToken;

		private bool _isHit; //safety if two destructible object overlaps. Then Destroy only one
	
		public void Init(Vector2 startPosition, Vector2 direction, IPoolManager<Bullet> pool)
		{
			_pool = pool;
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
			await UniTask.Delay(bulletLifeTimeInMS, cancellationToken: _returnToPoolCancellationToken.Token);
			ReturnToPool();
		}
	
		private void SetMovement(Vector2 direction)
		{
			_movement = new SimpleMovement();
			_movement.Init(transform, direction, bulletSpeed);
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