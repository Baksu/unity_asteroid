using System;
using UnityEngine;

namespace Data
{
	[CreateAssetMenu(fileName = "New rock data", menuName = "Data/Rock", order = 1)]
	[Serializable]
	public class RockData : ScriptableObject
	{
		[SerializeField] private int _startingRockCount;
		[SerializeField] private int _howManyRocksAddPerLevel;
		[SerializeField] private RockLevelData _firstRockLevelData;
		
		public int StartingRockCount => _startingRockCount;
		public int HowManyRocksAddPerLevel => _howManyRocksAddPerLevel;
		public RockLevelData FirstRockLevelData => _firstRockLevelData;
	}
}