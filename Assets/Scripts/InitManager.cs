using Data;
using DefaultNamespace;
using Managers;
using Managers.Interfaces;
using UnityEngine;

public class InitManager : MonoBehaviour
{
    [SerializeField] private BaseGameData _baseGameData;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private UiMainWindow _mainWindow;

    private IGameManager _gameManager;
    
    private void Start()
    {
        LoadGame();
    }

    private void LoadGame(){
        //turn on loader window no need here

        CreateManagers();
        // init all data
        
        EndLoad();
    }

    private void CreateManagers() //Here I'm using simple injection because it's small project but it can be done by some plugin like zenject
    {
        var uiManager = new UIManager();
        _mainWindow.Init(uiManager);

        //we can avoid creating here game object and have init class for managers. We need to create spawnManager or something like this
        var bulletsPool = new GameObject("Bullets Manager").AddComponent<BulletsPool>();
        bulletsPool.Init(_baseGameData.BaseBullet);

        var playerManager = new GameObject("Player Manager").AddComponent<PlayerManager>();
        playerManager.Init(_playerData, bulletsPool);
        
        _gameManager = new GameManager(_baseGameData, playerManager, uiManager);
    }

    private void EndLoad()
    {
        //turn off loader window
        _gameManager.Idle();
    }
    
    
}
