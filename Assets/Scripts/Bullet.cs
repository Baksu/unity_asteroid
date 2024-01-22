using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DefaultNamespace
{
	public class Bullet : MonoBehaviour
	{
		//TODO: move to scriptable object
		public float bulletSpeed = 50f;
		public int bulletLifeTimeInMS;
		
		private Vector2 _direction;
		
		public void Init(Vector2 direction)
		{
			_direction = direction;
			CountLifeTime().Forget();
		}

		private void FixedUpdate()
		{
			Move();
		}

		//todo: think if need to use rigidbody to move? Probably yes to not have glitches during moving
		private void Move()
		{
			Vector2 transformPosition = transform.position;
			transformPosition += _direction * bulletSpeed;
			transform.position = transformPosition;
		}

		private async UniTaskVoid CountLifeTime()
		{
			gameObject.GetCancellationTokenOnDestroy();
			await UniTask.Delay(bulletLifeTimeInMS);
			Destroy(gameObject); //TODO: hide and return to the bullet object pool. Remember to add flag in update checking if is spawn
		}
	}
}