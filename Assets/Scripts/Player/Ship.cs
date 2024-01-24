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
        
        private PlayerData _playerData;
        private IPoolManager<Bullet> _bulletsPool;
        
        public void Init(PlayerData playerData, IPoolManager<Bullet> bulletsManager)
        {
            _playerData = playerData;
            _bulletsPool = bulletsManager;
        }

        private void Update() 
        {
            if (Input.GetKeyDown(KeyCode.Space)) //TODO: add command design pattern for inputs. It helps when for example game has a settings when a player can change key configuration
            {
                Fire();
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
        
        private void Fire()
        {
            var bullet = _bulletsPool.GetObject();
            bullet.Init(_bulletSpawnPoint.position, transform.up, _bulletsPool);
        }
    }
}