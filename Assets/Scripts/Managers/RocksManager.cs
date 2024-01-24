using System;
using Data;
using Enemies;
using Managers.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Managers
{
	public class RocksManager : IRocksManager
	{
		public event Action<RockLevelData> OnRockDestroyed;
		public event Action OnRocksEndsAction;		
		
		private readonly RockData _rockData;
		private readonly Camera _mainCamera;

		private int _allRocksOnLevel;
		
		public RocksManager(RockData rockData)
		{
			_mainCamera = Camera.main;
			_rockData = rockData;
		}

		public void SpawnInitRock()
		{
			SpawnRocksForLevel(0);
		}

		public void SpawnRocksForLevel(int level)
		{
			for (int i = 0; i < _rockData.StartingRockCount + _rockData.HowManyRocksAddPerLevel * level; i++)
			{
				SpawnRock(_rockData.FirstRockLevelData, InitialSpawnPosition());
			}
		}

		private void SpawnRock(RockLevelData levelData, Vector2 spawnPosition)
		{
			var rock = Object.Instantiate(levelData.Prefab, spawnPosition, Quaternion.identity).GetComponent<Rock>();
			if (rock != null)
			{
				rock.Init(levelData, _mainCamera, OnRockDestroy);
				_allRocksOnLevel++;
			}
		}

		private void OnRockDestroy(RockLevelData levelData, Vector2 prevRockPosition)
		{
			OnRockDestroyed?.Invoke(levelData);
			_allRocksOnLevel--;

			TryFinishLevel();
			
			if (levelData.NextLevel == null)
			{
				return;
			}
			
			for (int i = 0; i < 2; i++)
			{
				SpawnRock(levelData.NextLevel, prevRockPosition);
			}
		}

		private void TryFinishLevel()
		{
			if (_allRocksOnLevel <= 0)
			{
				OnRocksEndsAction?.Invoke();
			}
		}

		private Vector2 InitialSpawnPosition()
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
		
			return _mainCamera.ViewportToWorldPoint(startViewportPosition);
		}
	}
}