using System;
using UnityEngine;

namespace DefaultNamespace
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
			var viewportPosition = _mainCamera.WorldToViewportPoint(transform.position);

			var moveAdjustment = Vector3.zero;
			if (viewportPosition.x < 0)
			{
				moveAdjustment.x += 1;
			}
			else if (viewportPosition.x > 1)
			{
				moveAdjustment.x -= 1;
			}
			else if( viewportPosition.y < 0)
			{
				moveAdjustment.y += 1;
			}
			else if (viewportPosition.y > 1)
			{
				moveAdjustment.y -= 1;
			}

			transform.position = _mainCamera.ViewportToWorldPoint(viewportPosition + moveAdjustment);
		}
	}
}