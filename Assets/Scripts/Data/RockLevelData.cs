using System;
using Data.Interfaces;
using UnityEngine;

namespace Data
{
	[CreateAssetMenu(fileName = "New rock level data", menuName = "Data/Rock Level", order = 1)]
	[Serializable]
	public class RockLevelData : ScriptableObject, IRockLevelData
	{
		[SerializeField] private GameObject _prefab;
		[SerializeField] private float _minSpeed;
		[SerializeField] private float _maxSpeed;
		[SerializeField] private RockLevelData _nextLevel;
		[SerializeField] private int _pointsForDestroy; 
		
		public GameObject Prefab => _prefab;
		public float MinSpeed => _minSpeed;
		public float MaxSpeed => _maxSpeed;
		public IRockLevelData NextLevel => _nextLevel;
		public int PointsForDestroy => _pointsForDestroy;
	}
}