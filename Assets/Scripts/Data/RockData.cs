using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
	[CreateAssetMenu(fileName = "New rock data", menuName = "Data/Rock", order = 1)]
	[Serializable]
	public class RockData : ScriptableObject
	{
		[SerializeField] private List<RockLevelData> _levels;
		public List<RockLevelData> Levels => _levels;
	}

	[Serializable]
	public class RockLevelData
	{
		[SerializeField] private GameObject _prefab;
		[SerializeField] private float _minSpeed;
		[SerializeField] private float _maxSpeed;

		public GameObject Prefab => _prefab;
		public float MinSpeed => _minSpeed;
		public float MaxSpeed => _maxSpeed;
	}
}