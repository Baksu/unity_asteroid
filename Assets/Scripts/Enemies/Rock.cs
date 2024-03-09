using System;
using Data.Interfaces;
using Enemies.Interfaces;
using Interfaces;
using Movement;
using Movement.Interfaces;
using Pool.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
	public class Rock : MonoBehaviour, IObstacle, IDestructible, IPoolObject
	{
		public IRockLevelData LevelData { get; private set; }
		public int CurrentLevel { get; private set; }
		
		private IMovement _movement;
		private Action<Rock, Vector2> _onRockDestroy;
		
		private void FixedUpdate()
		{
			_movement?.Move();
		}

		public void Init(int currentLevel, IRockLevelData levelData, Action<Rock, Vector2> onRockDestroy)
		{
			CurrentLevel = currentLevel;
			LevelData = levelData;
			_onRockDestroy = onRockDestroy;
			SetMovement();
		}

		private void SetMovement()
		{
			_movement = new SimpleMovement();
			var initDirection = Random.insideUnitCircle.normalized;
			var initSpeed = Random.Range(LevelData.MinSpeed, LevelData.MaxSpeed);
			_movement.Init(transform, initDirection, initSpeed);
		}

		public void Destroyed()
		{
			_onRockDestroy?.Invoke(this, transform.position);
		}

		public void AfterGet()
		{
			gameObject.SetActive(true);
		}

		public void BeforeRelease()
		{
			gameObject.SetActive(false);
		}
	}
}