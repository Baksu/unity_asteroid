using System;
using Data;

namespace Managers.Interfaces
{
	public interface IRocksManager
	{
		public event Action<RockLevelData> OnRockDestroyed;
		public event Action OnRocksEndsAction;
		public void SpawnInitRock();
		public void SpawnRocksForLevel(int level);
	}
}