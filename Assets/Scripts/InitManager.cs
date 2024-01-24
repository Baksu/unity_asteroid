using Data;
using DefaultNamespace;
using Managers;
using Managers.Interfaces;
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
        //TODO: turn on loader window no need here

        CreateManagers();
        // init all data
        
        EndLoad();
    }

    private void CreateManagers() //TODO: Here I'm using simple injection because it's small project but it can be done by some plugin like zenject
    {
        var uiManager = new UIManager();
        _mainWindow.Init(uiManager);

        //we can avoid creating here game object and have init class for managers. We need to create spawnManager or something like this
        var bulletsPool = new GameObject("Bullets Manager").AddComponent<BulletsPool>();
        bulletsPool.Init(_baseGameData.BaseBullet);

        var playerManager = new GameObject("Player Manager").AddComponent<PlayerManager>();
        playerManager.Init(_playerData, bulletsPool);

        var rocksManager = new RocksManager(rockData);
        var scoreManager = new ScoreManager(uiManager, rocksManager);
        
        _gameManager = new GameManager(_baseGameData, playerManager, uiManager, rocksManager, scoreManager);
    }

    private void EndLoad()
    {
        //TODO turn off loader window
        _gameManager.Idle();
    }
    
    
}
