using System;
using System.Collections.Generic;
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
		
		private readonly List<Rock> _rocksOnLevel = new ();
		
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

		public void ResetGameState()
		{
			DestroyOtherRocks();
		}

		private void DestroyOtherRocks()
		{
			foreach (var rock in _rocksOnLevel)
			{
				rock.Clear();
			}
			_rocksOnLevel.Clear();
		}

		private void SpawnRock(RockLevelData levelData, Vector2 spawnPosition)
		{
			var rock = Object.Instantiate(levelData.Prefab, spawnPosition, Quaternion.identity).GetComponent<Rock>();
			if (rock != null)
			{
				rock.Init(levelData, OnRockDestroy);
				_rocksOnLevel.Add(rock);
			}
		}

		private void OnRockDestroy(Rock rock, Vector2 prevRockPosition)
		{
			OnRockDestroyed?.Invoke(rock.LevelData);
			_rocksOnLevel.Remove(rock);
			
			TryFinishLevel();
			
			if (rock.LevelData.NextLevel == null)
			{
				return;
			}
			
			for (int i = 0; i < 2; i++)
			{
				SpawnRock(rock.LevelData.NextLevel, prevRockPosition);
			}
		}

		private void TryFinishLevel()
		{
			if (_rocksOnLevel.Count <= 0)
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