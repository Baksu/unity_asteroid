using System;

namespace Managers.Interfaces
{
	public interface IGameManager
	{
		public event EventHandler OnLiveChangedAction;
		public event EventHandler OnGameOver;
		
		public void Idle();
		public void StartGame();
	}
}