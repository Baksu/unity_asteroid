using System;
using Data;
using Managers.Interfaces;

namespace Managers
{
	public class GameManager : IGameManager, IDisposable
	{
		public event Action<int> OnLiveChangedAction;
		public event Action OnGameOver;
		
		private readonly BaseGameData _gameData;
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
				OnLiveChangedAction?.Invoke(_currentLives);
			}
		}

		public GameManager(BaseGameData gameData,
			IPlayerManager playerManager,
			IRocksManager rocksManager,
			IScoreManager scoreManager)
		{
			_gameData = gameData;
			_playerManager = playerManager;
			_rocksManager = rocksManager;
			_scoreManager = scoreManager;
			
			_playerManager.OnPlayerDestroyedAction += OnPlayerDestroyed;
			_rocksManager.OnRocksEndsAction += NextLevel;
		}
		
		public void Dispose()
		{
			_playerManager.OnPlayerDestroyedAction -= OnPlayerDestroyed;
			_rocksManager.OnRocksEndsAction -= NextLevel;
		}

		public void Idle()
		{
			ResetGameState();
		}

		private void ResetGameState()
		{
			_currentLevel = 0;
			CurrentLives = _gameData.StartLives;
			_scoreManager.ResetGameState();
			_rocksManager.ResetGameState();
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
			OnGameOver?.Invoke();
		}
	}
}