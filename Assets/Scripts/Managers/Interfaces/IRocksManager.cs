using System;

namespace Managers.Interfaces
{
	public interface IRocksManager
	{
		public event EventHandler OnRockDestroyed;
		public event EventHandler OnRocksEndsAction;
		public void SpawnInitRock();
		public void SpawnRocksForLevel(int level);
		public void ResetGameState();
	}
}