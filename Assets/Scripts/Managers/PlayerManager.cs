using System;
using Data;
using Managers.Interfaces;
using Player.Interface;
using UnityEngine;

namespace Managers
{
	public class PlayerManager : MonoBehaviour, IPlayerManager
	{
		public event Action OnPlayerDestroyedAction;
		
		private IPoolManager<Bullet> _bulletsPool;
		private PlayerData _playerData;
		
		public void Init(PlayerData playerData, IPoolManager<Bullet>  bulletsPool)
		{
			_bulletsPool = bulletsPool;
			_playerData = playerData;
		}

		public void SpawnPlayer()
		{
			if (Instantiate(_playerData.PlayerShipPrefab, Vector2.zero, Quaternion.identity).TryGetComponent(out IPlayer player))
			{
				player.Init(_playerData, _bulletsPool, OnPlayerDestroyed);
			}
		}

		public void OnPlayerDestroyed()
		{
			OnPlayerDestroyedAction?.Invoke();
		}
	}
}