using System;
using Data.Interfaces;
using UnityEngine;

namespace Data
{
	[CreateAssetMenu(fileName = "New player data", menuName = "Data/Player", order = 1)]
	[Serializable]
	public class PlayerData : ScriptableObject, IPlayerData
	{
		[SerializeField] private GameObject _playerShipPrefab;
		[SerializeField] private float _rotationSpeed = 5f;
		[SerializeField] private float _thrust = 3f;
		[SerializeField] private float _friction = 0.3f;
		[SerializeField] private float _minimumVelocityToMove = 0.2f;
		[SerializeField] private int _indestructibleAfterSpawnInMS = 1000;

		public GameObject PlayerShipPrefab => _playerShipPrefab;
		public float RotationSpeed => _rotationSpeed;
		public float Thrust => _thrust;
		public float Friction => _friction;
		public float MinimumVelocityToMove => _minimumVelocityToMove;
		public int IndestructibleAfterSpawnInMS => _indestructibleAfterSpawnInMS;
	}
}