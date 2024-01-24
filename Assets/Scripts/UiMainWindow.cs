using System;
using Cysharp.Threading.Tasks;
using Managers.Interfaces;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
	public class UiMainWindow : MonoBehaviour
	{
		[SerializeField] private GameObject _startText;
		[SerializeField] private TextMeshProUGUI _pointsText;
		[SerializeField] private TextMeshProUGUI _lives;

		private IUIManager _uiManager;
		private bool _isGameStarted;
		
		public void Init(IUIManager uiManager)
		{
			_uiManager = uiManager;
			SetListeners();
		}
		
		private async UniTaskVoid WaitForStart()
		{
			var ct = gameObject.GetCancellationTokenOnDestroy();
			await UniTask.WaitUntil( () => Input.GetKeyDown(KeyCode.Space), cancellationToken: ct);
			StartGame();
		}
		
		private void SetListeners()
		{
			_uiManager.UpdatePointsAction += UpdatePoints;
			_uiManager.UpdateLivesAction += UpdateLives;
			_uiManager.IdleAction += IdleGame;
			_uiManager.OnStartGameAction += GameStarted;
		}

		private void OnDestroy() 
		{
			_uiManager.UpdatePointsAction -= UpdatePoints;
			_uiManager.UpdateLivesAction -= UpdateLives;
			_uiManager.IdleAction -= IdleGame;
			_uiManager.OnStartGameAction -= GameStarted;
		}

		private void UpdatePoints(int currentPoints)
		{
			_pointsText.text = $"Points: {currentPoints}";
		}

		private void UpdateLives(int currentLives)
		{
			_lives.text = $"Lives: {currentLives}";
		}

		private void IdleGame()
		{
			_startText.SetActive(true);
			_isGameStarted = false;
			
			WaitForStart().Forget();
		}
		
		private void StartGame()
		{
			_startText.SetActive(false);
			
			_uiManager.StartGame();
		}

		private void GameStarted()
		{
			_isGameStarted = true;
		}
	}
}