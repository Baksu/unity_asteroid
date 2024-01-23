using Data;
using Managers.Interfaces;
using Player.Interface;
using UnityEngine;

namespace Managers
{
	public class PlayerManager : MonoBehaviour, IPlayerManager
	{
		private IPoolManager<Bullet> _bulletsPool;
		private PlayerData _playerData;

		public void Init(PlayerData playerData, IPoolManager<Bullet>  bulletsPool)
		{
			_bulletsPool = bulletsPool;
			_playerData = playerData;
		}

		public void SpawnPlayer()
		{
			//TODO add some protection to not have two players in the same time
			if (Instantiate(_playerData.PlayerShipPrefab, Vector2.zero, Quaternion.identity).TryGetComponent(out IPlayer player))
			{
				player.Init(_playerData, _bulletsPool);
			}
		}
	}
}