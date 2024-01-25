using System;
using Data;
using Enemies.Interfaces;
using Interfaces;
using Movement;
using Movement.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
	public class Rock : MonoBehaviour, IObstacle, IDestructible
	{
		public RockLevelData LevelData => _levelData;
		
		private IMovement _movement;
		private RockLevelData _levelData;
		private Action<Rock, Vector2> _onRockDestroy;
		
		private void FixedUpdate()
		{
			_movement?.Move();
		}

		public void Init(RockLevelData levelData, Action<Rock, Vector2> onRockDestroy)
		{
			_levelData = levelData;
			_onRockDestroy = onRockDestroy;
			SetMovement();
		}

		private void SetMovement()
		{
			_movement = new SimpleMovement();
			var initDirection = Random.insideUnitCircle.normalized;
			var initSpeed = Random.Range(_levelData.MinSpeed, _levelData.MaxSpeed);
			_movement.Init(transform, initDirection, initSpeed);
		}

		public void Destroyed()
		{
			_onRockDestroy?.Invoke(this, transform.position);
			Clear();
		}

		public void Clear()
		{
			Destroy(gameObject);
		}
	}
}