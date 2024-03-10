using System;
using Managers.Interfaces;
using Player;
using Player.Interfaces;
using Pool.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Managers
{
	public class PlayerManager : IPlayerManager
	{
		public event EventHandler OnPlayerDestroyed;
		
		private readonly IPoolManager<Bullet> _bulletsPool;
		private readonly IDataManager _dataManager;

		private IPlayer _currentPlayer;
		
		public PlayerManager(IDataManager dataManager, IPoolManager<Bullet>  bulletsPool)
		{
			_bulletsPool = bulletsPool;
			_dataManager = dataManager;
		}

		public void CreatePlayer()
		{
			if (Object.Instantiate(_dataManager.PlayerData.PlayerShipPrefab, Vector2.zero, Quaternion.identity).TryGetComponent(out _currentPlayer))
			{
				_currentPlayer.Init(_dataManager.PlayerData, _bulletsPool, _dataManager);
				_currentPlayer.OnPlayerDestroyed += OnPlayerDestroyed;
			}
		}

		public void SpawnPlayer()
		{
			_currentPlayer?.SpawnShip();
		}
	}
}