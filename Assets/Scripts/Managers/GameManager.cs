using Data;
using Managers.Interfaces;

namespace Managers
{
	public class GameManager : IGameManager
	{
		private readonly BaseGameData _gameData;
		private readonly IUIManager _uiManager;
		private readonly IPlayerManager _playerManager;

		private int _currentPoints;
		private int CurrentPoints
		{
			get => _currentPoints;
			set
			{
				_currentPoints = value;
				_uiManager.UpdatePoints(_currentPoints);
			}
		}

		private int _currentLives;
		private int CurrentLives
		{
			get => _currentLives;
			set
			{
				_currentLives = value;
				_uiManager.UpdateLives(_currentLives);
			}
		}

		public GameManager(BaseGameData gameData, IPlayerManager playerManager, IUIManager uiManager)
		{
			_gameData = gameData;
			_uiManager = uiManager;
			_playerManager = playerManager;

			_uiManager.StartGameAction += StartGame;
		}
		
		public void Idle()
		{
			_uiManager.Idle();
			ResetGameState();
		}

		public void StartGame()
		{
			_playerManager.SpawnPlayer();
		}

		private void ResetGameState()
		{
			CurrentPoints = 0;
			CurrentLives = _gameData.StartLives;
		}

	}
}