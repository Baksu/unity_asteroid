using System;

namespace Managers.Interfaces
{
	public interface IPlayerManager
	{
		public event EventHandler OnPlayerDestroyed;
		public void SpawnPlayer();
	}
}