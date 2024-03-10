using System;
using Data.Interfaces;
using Managers.Interfaces;
using Pool.Interfaces;

namespace Player.Interfaces
{
	public interface IPlayer
	{
		public event EventHandler OnPlayerDestroyed;
		public void Init(IPlayerData playerData, IPoolManager<Bullet> bulletsManager, IDataManager dataManager);
		public void SpawnShip();
		public void Rotate(float direction);
		public void Accelerate();
		public void Fire();
	}
}