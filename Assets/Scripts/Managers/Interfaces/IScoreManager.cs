using System;

namespace Managers.Interfaces
{
	public interface IScoreManager
	{
		public event Action<int> OnPointsUpdate;
		public int GetScore();
		public void ResetGameState();
	}
}