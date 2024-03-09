using System;
using System.Collections.Generic;
using Data.Interfaces;
using Enemies;
using Managers.Interfaces;
using Pool;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Managers
{
	public class OnRockDestroyedEventArgs : EventArgs
	{
		public IRockLevelData RockLevelData { get; set; }
	}
	
	public class RocksManager : IRocksManager
	{
		public event EventHandler OnRockDestroyed;
		public event EventHandler OnRocksEndsAction;		
		
		private readonly IDataManager _dataManager;
		private readonly Camera _mainCamera;
		
		private readonly List<Rock> _rocksOnLevel = new ();

		private List<RocksPool> _rocksPools = new ();
		
		public RocksManager(IDataManager dataManager)
		{
			_mainCamera = Camera.main;
			_dataManager = dataManager;

			CreateRocksPools();
		}

		private void CreateRocksPools()
		{
			var rockData = _dataManager.RockData.FirstRockLevelData;
			while (rockData != null)
			{
				_rocksPools.Add(new RocksPool(rockData.Prefab));
				rockData = rockData.NextLevel;
			}
		}

		public void SpawnInitRock()
		{
			SpawnRocksForLevel(0);
		}

		public void SpawnRocksForLevel(int gameLevel)
		{
			for (int i = 0; i < _dataManager.RockData.StartingRockCount + _dataManager.RockData.HowManyRocksAddPerLevel * gameLevel; i++)
			{
				SpawnRock(0, _dataManager.RockData.FirstRockLevelData, InitialSpawnPosition());
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
				_rocksPools[rock.CurrentLevel].ReleaseObject(rock);
			}
			_rocksOnLevel.Clear();
		}

		private void SpawnRock(int rockLevel, IRockLevelData levelData, Vector2 spawnPosition)
		{
			var rock = _rocksPools[rockLevel].GetObject();
			rock.transform.position = spawnPosition;
			if (rock != null)
			{
				rock.Init(rockLevel, levelData, OnRockDestroy);
				_rocksOnLevel.Add(rock);
			}
		}

		private void OnRockDestroy(Rock rock, Vector2 prevRockPosition)
		{
			OnRockDestroyed?.Invoke(this, new OnRockDestroyedEventArgs{ RockLevelData = rock.LevelData});
			_rocksPools[rock.CurrentLevel].ReleaseObject(rock);
			_rocksOnLevel.Remove(rock);
			
			TryFinishLevel();
			
			if (rock.LevelData.NextLevel == null)
			{
				return;
			}
			
			for (int i = 0; i < 2; i++)
			{
				SpawnRock(rock.CurrentLevel + 1, rock.LevelData.NextLevel, prevRockPosition);
			}
		}

		private void TryFinishLevel()
		{
			if (_rocksOnLevel.Count <= 0)
			{
				OnRocksEndsAction?.Invoke(this, EventArgs.Empty);
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