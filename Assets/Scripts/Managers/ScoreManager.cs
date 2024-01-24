using System;
using Data;
using Managers.Interfaces;

namespace Managers
{
	public class ScoreManager : IScoreManager, IDisposable
	{
		private readonly IUIManager _uiManager;
		private readonly IRocksManager _rocksManager;

		private int _currentScore;

		private int CurrentScore
		{
			get => _currentScore;
			set
			{
				_currentScore = value;
				_uiManager.UpdatePoints(_currentScore);
			}
		}
		
		public ScoreManager(IUIManager uiManager, IRocksManager rocksManager)
		{
			_uiManager = uiManager;
			_rocksManager = rocksManager;
			_rocksManager.OnRockDestroyed += OnRockDestroyed;
		}
		
		public void Dispose()
		{
			_rocksManager.OnRockDestroyed -= OnRockDestroyed;
		}

		private void OnRockDestroyed(RockLevelData rockLevelData)
		{
			CurrentScore += rockLevelData.PointsForDestroy;
		}

		public void ResetGameState()
		{
			CurrentScore = 0;
		}
	}
}