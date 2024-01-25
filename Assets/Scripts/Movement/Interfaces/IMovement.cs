using UnityEngine;

namespace Movement.Interfaces
{
	public interface IMovement
	{
		public void Init(Transform transform, Vector2 initDirection, float initSpeed);
		public void Move();
	}
}