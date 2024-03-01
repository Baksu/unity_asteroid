using System;

namespace Managers.Interfaces
{
	public interface IScoreManager
	{
		public event EventHandler OnPointsUpdate;
		public int GetScore();
		public void ResetGameState();
	}
}