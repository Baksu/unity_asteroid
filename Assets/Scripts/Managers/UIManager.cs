using System;
using Managers.Interfaces;

namespace Managers
{
	public class UIManager : IUIManager
	{
		public event Action<int> UpdatePointsAction;
		public event Action<int> UpdateLivesAction;
		public event Action IdleAction;
		public event Action StartGameAction;
		
		public void Idle()
		{
			IdleAction?.Invoke();
		}
		
		public void StartGame()
		{ 
			StartGameAction?.Invoke();
		}

		public void UpdatePoints(int points)
		{
			UpdatePointsAction?.Invoke(points);
		}

		public void UpdateLives(int lives)
		{
			UpdateLivesAction?.Invoke(lives);
		}


	}
}