using System;
using Data;
using Managers.Interfaces;

namespace Managers
{
	public class GameManager : IGameManager, IDisposable
	{
		private readonly BaseGameData _gameData;
		private readonly IUIManager _uiManager;
		private readonly IPlayerManager _playerManager;
		private readonly IRocksManager _rocksManager;
		private readonly IScoreManager _scoreManager;

		private int _currentLevel;

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

		public GameManager(BaseGameData gameData,
			IPlayerManager playerManager,
			IUIManager uiManager,
			IRocksManager rocksManager,
			IScoreManager scoreManager)
		{
			_gameData = gameData;
			_uiManager = uiManager;
			_playerManager = playerManager;
			_rocksManager = rocksManager;
			_scoreManager = scoreManager;

			_uiManager.OnStartGameAction += StartGame;
			_playerManager.OnPlayerDestroyedAction += OnPlayerDestroyed;
			_rocksManager.OnRocksEndsAction += NextLevel;
		}
		
		public void Dispose()
		{
			_uiManager.OnStartGameAction -= StartGame;
			_playerManager.OnPlayerDestroyedAction -= OnPlayerDestroyed;
			_rocksManager.OnRocksEndsAction -= NextLevel;
		}
		
		public void Idle()
		{
			_uiManager.Idle();
			ResetGameState();
		}

		private void ResetGameState()
		{
			_currentLevel = 0;
			CurrentLives = _gameData.StartLives;
			_scoreManager.ResetGameState();
		}
		
		public void StartGame()
		{
			_playerManager.SpawnPlayer();
			_rocksManager.SpawnInitRock();
		}

		private void NextLevel()
		{
			_currentLevel++;
			_rocksManager.SpawnRocksForLevel(_currentLevel);
		}

		private void OnPlayerDestroyed()
		{
			CurrentLives--;
			if (CurrentLives <= 0)
			{
				GameOver();
				return;
			}
			_playerManager.SpawnPlayer();
		}

		private void GameOver()
		{
			
		}
	}
}