using System;

namespace Managers.Interfaces
{
	public interface IGameManager
	{
		public event Action<int> OnLiveChangedAction;
		public event Action OnGameOver;
		
		public void Idle();
		public void StartGame();
	}
}