using UnityEngine;

namespace DefaultNamespace.Interfaces
{
	public interface IMovement
	{
		public void Init(Transform transform, Vector2 initDirection, float initSpeed);
		public void Move();
	}
}