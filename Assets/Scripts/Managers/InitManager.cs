using Managers;
using Managers.Interfaces;
using Pool;
using UI;
using UnityEngine;

public class InitManager : MonoBehaviour
{
    [SerializeField] private DataManager _dataManager;
    [SerializeField] private UiMainWindow _mainWindow;

    private IGameManager _gameManager;
    
    private void Start()
    {
        LoadGame();
    }

    private void LoadGame(){
        CreateManagers();
        EndLoad();
    }

    private void CreateManagers() //I'm using simple injection because it's small project but it can be done by some plugin like zenject
    {
        var bulletsPool = new BulletsPool(_dataManager.BulletData.BaseBulletPrefab);
        var playerManager = new PlayerManager(_dataManager, bulletsPool); 
        var rocksManager = new RocksManager(_dataManager.RockData);
        var scoreManager = new ScoreManager(rocksManager);
        _gameManager = new GameManager(_dataManager, playerManager, rocksManager, scoreManager);
        _mainWindow.Init(scoreManager, _gameManager);
    }

    private void EndLoad()
    {
        _gameManager.Idle();
    }
}
