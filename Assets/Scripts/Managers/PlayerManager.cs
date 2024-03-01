using System;
using Data;
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
		
		public PlayerManager(IDataManager dataManager, IPoolManager<Bullet>  bulletsPool)
		{
			_bulletsPool = bulletsPool;
			_dataManager = dataManager;
		}

		public void SpawnPlayer()
		{
			if (Object.Instantiate(_dataManager.PlayerData.PlayerShipPrefab, Vector2.zero, Quaternion.identity).TryGetComponent(out IPlayer player))
			{
				player.Init(_dataManager.PlayerData, _bulletsPool);
				player.OnPlayerDestroyed += OnPlayerDestroyed;
			}
		}
	}
}