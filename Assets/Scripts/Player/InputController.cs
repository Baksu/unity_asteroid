using Player.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
	public class InputController : MonoBehaviour
	{
		private IPlayer _player;

		private bool _isAccelerate;
		private Vector2 _direction;
		
		public void Init(IPlayer player)
		{
			_player = player;
		}
		
		public void OnAcceleration(InputValue value)
		{
			_isAccelerate = value.isPressed;
		}

		public void OnRotation(InputValue value)
		{
			_direction = value.Get<Vector2>();
		}

		public void OnFire(InputValue value)
		{
			_player?.Fire();
		}

		private void FixedUpdate()
		{
			if (_isAccelerate)
			{
				_player?.Accelerate();
			}

			if (_direction.x != 0)
			{
				_player?.Rotate(_direction.x);
			}
		}
	}
}