using System;
using Cysharp.Threading.Tasks;
using Managers;
using Managers.Interfaces;
using TMPro;
using UnityEngine;

namespace UI
{
	public class UiMainWindow : MonoBehaviour
	{
		[SerializeField] private GameObject _clickSpaceGo;
		[SerializeField] private TextMeshProUGUI _pointsText;
		[SerializeField] private TextMeshProUGUI _livesText;

		[SerializeField] private GameObject _gameOverGo;
		[SerializeField] private TextMeshProUGUI _scoreText;
		
		private IScoreManager _scoreManager;
		private IGameManager _gameManager;
		
		public void Init(IScoreManager scoreManager, IGameManager gameManager)
		{
			_scoreManager = scoreManager;
			_gameManager = gameManager;
			SetListeners();

			IdleGame();
		}
		
		private async UniTaskVoid WaitForSpaceDown(Action action)
		{
			var ct = gameObject.GetCancellationTokenOnDestroy();
			await UniTask.WaitUntil( () => Input.GetKeyDown(KeyCode.Space), cancellationToken: ct);
			action?.Invoke();
		}
		
		private void SetListeners()
		{
			_scoreManager.OnPointsUpdate += UpdatePoints;
			_gameManager.OnLiveChangedAction += UpdateLives;
			_gameManager.OnGameOver += GameOver;
		}

		private void OnDestroy() 
		{
			_scoreManager.OnPointsUpdate -= UpdatePoints;
			_gameManager.OnLiveChangedAction -= UpdateLives;
			_gameManager.OnGameOver -= GameOver;
		}

		private void UpdatePoints(object sender, EventArgs eventArgs)
		{
			if (eventArgs is not OnPointsUpdateArgs args)
			{
				Debug.LogError("Wrong event arguments passed");
				return;
			}
			_pointsText.SetText($"Points: {args.Points}");
		}

		private void UpdateLives(object sender, EventArgs eventArgs)
		{
			if (eventArgs is not OnLiveChangedActionEventArgs args)
			{
				Debug.LogError("Wrong event arguments passed");
				return;
			}
			_livesText.SetText($"Lives: {args.Lives}");
		}

		private void IdleGame()
		{
			_gameManager.Idle();
			_clickSpaceGo.SetActive(true);
			_gameOverGo.SetActive(false);
			WaitForSpaceDown(StartGame).Forget();
		}
		
		private void StartGame()
		{
			_clickSpaceGo.SetActive(false);
			_gameManager.StartGame();
		}

		private void GameOver(object sender, EventArgs eventArgs)
		{
			_gameOverGo.SetActive(true);
			_clickSpaceGo.SetActive(true);
			_scoreText.SetText($"YOUR SCORE : {_scoreManager.GetScore()}");
			WaitForSpaceDown(IdleGame).Forget();
		}
	}
}