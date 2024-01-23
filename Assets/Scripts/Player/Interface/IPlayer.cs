using Data;
using Managers.Interfaces;

namespace Player.Interface
{
	public interface IPlayer
	{
		public void Init(PlayerData playerData, IPoolManager<Bullet> bulletsManager);
	}
}