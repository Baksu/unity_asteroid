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
		private readonly PlayerData _playerData;
		
		public PlayerManager(PlayerData playerData, IPoolManager<Bullet>  bulletsPool)
		{
			_bulletsPool = bulletsPool;
			_playerData = playerData;
		}

		public void SpawnPlayer()
		{
			if (Object.Instantiate(_playerData.PlayerShipPrefab, Vector2.zero, Quaternion.identity).TryGetComponent(out IPlayer player))
			{
				player.Init(_playerData, _bulletsPool);
				player.OnPlayerDestroyed += OnPlayerDestroyed;
			}
		}
	}
}