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
			var cToken = gameObject.GetCancellationTokenOnDestroy();
			await UniTask.WaitUntil( () => Input.GetKeyDown(KeyCode.Space), cancellationToken: cToken);
			StartGame();
		}
		
		private void SetListeners()
		{
			_uiManager.UpdatePointsAction += UpdatePoints;
			_uiManager.UpdateLivesAction += UpdateLives;
			_uiManager.IdleAction += IdleGame;
			_uiManager.StartGameAction += GameStarted;
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