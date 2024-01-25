using UnityEngine;

namespace Movement
{
	public class Teleporter : MonoBehaviour
	{

		private Camera _mainCamera;
		
		private void Start()
		{
			_mainCamera = Camera.main;
		}

		public void Update()
		{
			TryMoveToOtherSide();
		}

		private void TryMoveToOtherSide()
		{
			var viewportPosition = _mainCamera.WorldToViewportPoint(transform.position);

			var moveAdjustment = Vector3.zero;
			
			switch (viewportPosition.x)
			{
				case < 0:
					moveAdjustment.x += 1;
					break;
				case > 1:
					moveAdjustment.x -= 1;
					break;
			}
			
			switch (viewportPosition.y)
			{
				case < 0:
					moveAdjustment.y += 1;
					break;
				case > 1:
					moveAdjustment.y -= 1;
					break;
			}

			if (moveAdjustment != Vector3.zero)
			{
				transform.position = _mainCamera.ViewportToWorldPoint(viewportPosition + moveAdjustment);
			}
		}
	}
}