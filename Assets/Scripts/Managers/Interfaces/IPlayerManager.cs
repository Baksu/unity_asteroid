using System;

namespace Managers.Interfaces
{
	public interface IPlayerManager
	{
		public event Action OnPlayerDestroyedAction;
		public void SpawnPlayer();
	}
}