using Data;
using Managers;
using Managers.Interfaces;
using Pool;
using UI;
using UnityEngine;

public class InitManager : MonoBehaviour
{
    //TODO: here should be a separate manager for all data. I didn't had time to do that but it would be done similar to other managers
    [SerializeField] private BaseGameData _baseGameData;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private RockData rockData;
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

    private void CreateManagers() //TODO: Here I'm using simple injection because it's small project but it can be done by some plugin like zenject
    {
        var bulletsPool = new BulletsPool(_baseGameData.BaseBullet);
        var playerManager = new PlayerManager(_playerData, bulletsPool); 
        var rocksManager = new RocksManager(rockData);
        var scoreManager = new ScoreManager(rocksManager);
        _gameManager = new GameManager(_baseGameData, playerManager, rocksManager, scoreManager);
        _mainWindow.Init(scoreManager, _gameManager);
    }

    private void EndLoad()
    {
        _gameManager.Idle();
    }
}
