using Cysharp.Threading.Tasks;
using Data;
using Managers.Interfaces;
using Player.Interface;
using UnityEngine;

namespace Player
{
    public class Ship : MonoBehaviour, IPlayer
    {
        [SerializeField] private Rigidbody2D _rig;
        [SerializeField] private Transform _bulletSpawnPoint;
        
        private bool _isFiring;

        private PlayerData _playerData;
        private IPoolManager<Bullet> _bulletsPool;
        
        public void Init(PlayerData playerData, IPoolManager<Bullet> bulletsManager)
        {
            _playerData = playerData;
            _bulletsPool = bulletsManager;
        }

        private void Update() 
        {
            if (Input.GetKeyDown(KeyCode.Space) && !_isFiring) //TODO: add command design patter for inputs
            {
                _isFiring = true;
                Fire().Forget();
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                _isFiring = false;
            }
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Rotate(true);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Rotate(false);
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                Accelerate();
            }
            
            Friction();
        }

        private void Rotate(bool rightDirection)
        {
            float direction = rightDirection ? -1 : 1;
            _rig.rotation += direction * _playerData.RotationSpeed;
        }

        private void Accelerate()
        {
            _rig.AddForce(transform.up * _playerData.Thrust);
        }

        private void Friction()
        {
            if (_rig.velocity.magnitude >= _playerData.MinimumVelocityToMove) //If ship is moving add friction
            {
                _rig.AddForce(-1 * _playerData.Friction * _rig.velocity);
            }
            else if (_rig.velocity.magnitude > 0.0f)
            {
                _rig.velocity = Vector2.zero;
            }
        }
        
        private async UniTaskVoid Fire()
        {
            while (_isFiring)
            {
                var bullet = _bulletsPool.GetObject();
                bullet.Init(_bulletSpawnPoint.position, transform.up, _bulletsPool);
                await UniTask.Delay(_playerData.DelayBetweenShotsInMS); //TODO add two cancelation token for destroy and for stop fire
            }
        }
    }
}