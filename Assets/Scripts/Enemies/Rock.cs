using System;
using Data;
using DefaultNamespace.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
	public class Rock : MonoBehaviour, IObstacle, IDestructible
	{
		private Camera _mainCamera;
		private IMovement _movement;
		
		private RockLevelData _levelData;

		private Action<RockLevelData, Vector2> _onRockDestroy;
		
		private void FixedUpdate()
		{
			if (_movement != null)
			{
				_movement.Move();
			}
		}

		public void Init(RockLevelData levelData, Camera mainCamera, Action<RockLevelData, Vector2> onRockDestroy)
		{
			_levelData = levelData;
			_mainCamera = mainCamera;
			_onRockDestroy = onRockDestroy;
			SetMovement();
		}

		private void SetMovement()
		{
			_movement = new SimpleMovement();
			Vector2 initDirection = Random.insideUnitCircle.normalized;
			float initSpeed = Random.Range(_levelData.MinSpeed, _levelData.MaxSpeed);
			_movement.Init(transform, initDirection, initSpeed);
		}

		public void Destroyed()
		{
			_onRockDestroy?.Invoke(_levelData, transform.position);
			Destroy(gameObject);
		}
	}
}