using System;
using Data;

namespace Managers.Interfaces
{
	public interface IPlayerManager
	{
		public event Action OnPlayerDestroyedAction;
		public void Init(PlayerData playerData, IPoolManager<Bullet> bulletsPool);
		public void SpawnPlayer();
	}
}