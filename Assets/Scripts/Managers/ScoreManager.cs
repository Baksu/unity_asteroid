using System;
using Data;
using Managers.Interfaces;

namespace Managers
{
	public class ScoreManager : IScoreManager, IDisposable
	{
		public event Action<int> OnPointsUpdate;
		
		private readonly IRocksManager _rocksManager;

		private int _currentScore;
		public int GetScore() => _currentScore;
		
		private int CurrentScore
		{
			get => _currentScore;
			set
			{
				_currentScore = value;
				OnPointsUpdate?.Invoke(_currentScore);
			}
		}
		
		public ScoreManager(IRocksManager rocksManager)
		{
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