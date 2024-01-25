using Movement.Interfaces;
using UnityEngine;

namespace Movement
{
	public class SimpleMovement : IMovement
	{
		private Vector2 _direction;
		private float _speed;
		private Transform _transform;
		
		public void Init(Transform transform, Vector2 initDirection, float initSpeed)
		{
			_transform = transform;
			_direction = initDirection;
			_speed = initSpeed;
		}

		public void Move()
		{
			Vector2 transformPosition = _transform.position;
			transformPosition +=  _speed * Time.deltaTime * _direction;
			_transform.position = transformPosition;
		}
	}
}