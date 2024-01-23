using Cysharp.Threading.Tasks;
using DefaultNamespace.Interfaces;
using Managers.Interfaces;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolObject
{
	//TODO: move to scriptable object
	public float bulletSpeed = 40f;
	public int bulletLifeTimeInMS;
		
	private IMovement _movement;
	private IPoolManager<Bullet> _pool;
		
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
		if (other.gameObject.TryGetComponent<IDestructible>(out var hitObject))
		{
			Hit(hitObject);
		}
	}
	
	private void Hit(IDestructible other)
	{
		other.Destroyed();
	}

	private async UniTaskVoid CountLifeTime()
	{
		gameObject.GetCancellationTokenOnDestroy();
		await UniTask.Delay(bulletLifeTimeInMS);
		ReturnToPool();
	}
	
	private void SetMovement(Vector2 direction)
	{
		_movement = new SimpleMovement();
		_movement.Init(transform, direction, bulletSpeed);
	}

	private void ReturnToPool()
	{
		_pool.ReleaseObject(this);
	}
	
	public void AfterGet()
	{
		gameObject.SetActive(true);
	}

	public void BeforeRelease()
	{
		gameObject.SetActive(false);
	}
}