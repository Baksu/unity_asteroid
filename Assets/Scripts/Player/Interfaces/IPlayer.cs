using System;
using Data.Interfaces;
using Pool.Interfaces;

namespace Player.Interfaces
{
	public interface IPlayer
	{
		public event EventHandler OnPlayerDestroyed;
		public void Init(IPlayerData playerData, IPoolManager<Bullet> bulletsManager);
	}
}