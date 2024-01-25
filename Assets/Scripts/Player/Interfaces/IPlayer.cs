using System;
using Data;
using Pool.Interfaces;

namespace Player.Interfaces
{
	public interface IPlayer
	{
		public void Init(PlayerData playerData, IPoolManager<Bullet> bulletsManager, Action onPlayerDestroyed);
	}
}