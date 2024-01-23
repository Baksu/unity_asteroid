using Data;

namespace Managers.Interfaces
{
	public interface IPlayerManager
	{
		public void Init(PlayerData playerData, IPoolManager<Bullet> bulletsPool);
		public void SpawnPlayer();
	}
}