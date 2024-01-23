using System;

namespace Managers.Interfaces
{
	public interface IUIManager
	{
		public event Action<int> UpdatePointsAction;    
		public event Action<int> UpdateLivesAction;

		public event Action IdleAction;
		public event Action StartGameAction;

		public void Idle();
		public void StartGame();
		public void UpdatePoints(int points);
		public void UpdateLives(int lives);
	}
}