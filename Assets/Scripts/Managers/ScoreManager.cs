using System;
using Managers.Interfaces;
using UnityEngine;

namespace Managers
{
	public class OnPointsUpdateArgs : EventArgs
	{
		public int Points { get; set; }
	}
	
	public class ScoreManager : IScoreManager, IDisposable
	{
		public event EventHandler OnPointsUpdate;
		
		private readonly IRocksManager _rocksManager;

		private int _currentScore;
		public int GetScore() => _currentScore;
		
		private int CurrentScore
		{
			get => _currentScore;
			set
			{
				_currentScore = value;
				OnPointsUpdate?.Invoke(this, new OnPointsUpdateArgs{ Points = _currentScore});
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

		private void OnRockDestroyed(object sender, EventArgs eventArgs)
		{
			if (eventArgs is not OnRockDestroyedEventArgs args)
			{
				Debug.LogError("Wrong event arguments passed");
				return;
			}
			CurrentScore += args.RockLevelData.PointsForDestroy;
		}

		public void ResetGameState()
		{
			CurrentScore = 0;
		}
	}
}