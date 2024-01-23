using System.Linq;
using Data;
using DefaultNamespace.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
	public class Rock : MonoBehaviour, IObstacle
	{
		public RockData _toRemove;//TODO to remove
		public int currentLevel; //TODO to remove
		
		private Camera _mainCamera;
		private IMovement _movement;

		private RockLevelData _currentLevel => _toRemove.Levels.First();
		
		private void Start()
		{
			_mainCamera = Camera.main;
			Init();
		}

		private void FixedUpdate()
		{
			if (_movement != null)
			{
				_movement.Move();
			}
		}

		private void Init()
		{
			SetStartPosition();
			SetMovement();
		}

		private void SetStartPosition()
		{
			var randomSpawnSide = Random.Range(0, 2);

			Vector3 startViewportPosition;
			
			if (randomSpawnSide == 0) //left-right
			{
				startViewportPosition = new Vector3(Random.Range(0, 2), Random.Range(0.0f, 1.0f), _mainCamera.nearClipPlane);
			}
			else //top-down
			{
				startViewportPosition = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0, 2), _mainCamera.nearClipPlane);
			}
			
			transform.position = _mainCamera.ViewportToWorldPoint(startViewportPosition);
		}

		private void SetMovement()
		{
			_movement = new SimpleMovement();
			Vector2 initDirection = Random.insideUnitCircle.normalized;
			float initSpeed = Random.Range(_currentLevel.MinSpeed, _currentLevel.MaxSpeed);
			_movement.Init(transform, initDirection, initSpeed);
		}
	}
}