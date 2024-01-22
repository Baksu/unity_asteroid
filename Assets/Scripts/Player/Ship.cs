using System;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using UnityEngine;

namespace Player
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rig;
        [SerializeField] private Transform _bulletSpawnPoint;

        //TODO: create pool object for bullet and inject them to ship and enemy
        public GameObject bulletPrefab;
        
        
        //TODO: move to the scriptable object
        public float rotationSpeed = 5f;
        public float thrust = 3f;
        public float friction = 0.3f;
        public float minimumVelocityToMove = 0.2f;
        public int delayBetweenShotsInMS = 1000;

        private bool _isFiring;
        
        public void Init()
        {
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !_isFiring)
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
            _rig.rotation += direction * rotationSpeed;
        }

        private void Accelerate()
        {
            _rig.AddForce(transform.up * thrust);
        }

        private void Friction()
        {
            if (_rig.velocity.magnitude >= minimumVelocityToMove) //If ship is moving add friction
            {
                _rig.AddForce(-1 * friction * _rig.velocity);
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
                var bullet = Instantiate(bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity).GetComponent<Bullet>();
                bullet.Init(transform.up);
                await UniTask.Delay(delayBetweenShotsInMS); //TODO add two cancelation token for destroy and for stop fire
            }
        }
    }

}