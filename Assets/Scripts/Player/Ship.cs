using System;
using Cysharp.Threading.Tasks;
using Data.Interfaces;
using Enemies.Interfaces;
using Player.Interfaces;
using Pool.Interfaces;
using Unity.Mathematics;
using UnityEngine;

namespace Player
{
    public class Ship : MonoBehaviour, IPlayer
    {
        [SerializeField] private Rigidbody2D _rig;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Transform _bulletSpawnPoint;

        [SerializeField] private Color _indestructibleColor;
        [SerializeField] private Color _destructibleColor;

        [SerializeField] private InputController _input;
        
        public event EventHandler OnPlayerDestroyed;
        
        private IPlayerData _playerData;
        private IPoolManager<Bullet> _bulletsPool;
        private bool _isSpawned;
        
        public void Init(IPlayerData playerData, IPoolManager<Bullet> bulletsManager)
        {
            gameObject.SetActive(false);
            _playerData = playerData;
            _bulletsPool = bulletsManager;
            _input.Init(this);
        }
        
        private async UniTaskVoid ShortIndestructible()
        {
            var ct = gameObject.GetCancellationTokenOnDestroy();
            _collider.enabled = false;
            _spriteRenderer.color = _indestructibleColor;
            await UniTask.Delay(_playerData.IndestructibleAfterSpawnInMS, cancellationToken: ct);
            _spriteRenderer.color = _destructibleColor;
            _collider.enabled = true;
        }

        private void FixedUpdate()
        {
            Friction();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            TryHit(other.gameObject);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            TryHit(other.gameObject);
        }

        private void TryHit(GameObject other)
        {
            if (_isSpawned && other.gameObject.TryGetComponent<IObstacle>(out var hitObject))
            {
                hitObject.Destroyed();
                DestroyShip();
            }
        }

        private void DestroyShip()
        {
            _isSpawned = false;
            gameObject.SetActive(false);
            OnPlayerDestroyed?.Invoke(this, EventArgs.Empty);
        }

        public void SpawnShip()
        {
            gameObject.SetActive(true);
            transform.position = Vector2.zero;
            transform.rotation = Quaternion.identity;;
            ShortIndestructible().Forget();
            _isSpawned = true;
        }

        private void Friction()
        {
            if (_rig.velocity.magnitude >= _playerData.MinimumVelocityToMove) //If ship is moving add friction
            {
                _rig.AddForce(-1 * _playerData.Friction * _rig.velocity);
            }
            // else if (_rig.velocity.magnitude > 0.0f)
            // {
            //     _rig.velocity = Vector2.zero;
            // }
        }
        
        public void Fire()
        {
            var bullet = _bulletsPool.GetObject();
            bullet.Init(_bulletSpawnPoint.position, transform.up, _bulletsPool);
        }
        
        public void Rotate(float direction)
        {
            _rig.rotation += -direction * _playerData.RotationSpeed;
        }

        public void Accelerate()
        {
            _rig.AddForce(transform.up * _playerData.Thrust);
        }
    }
}