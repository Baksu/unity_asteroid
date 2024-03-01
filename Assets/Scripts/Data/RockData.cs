using System;
using Data.Interfaces;
using UnityEngine;

namespace Data
{
	[CreateAssetMenu(fileName = "New rock data", menuName = "Data/Rock", order = 1)]
	[Serializable]
	public class RockData : ScriptableObject, IRockData
	{
		[SerializeField] private int _startingRockCount;
		[SerializeField] private int _howManyRocksAddPerLevel;
		[SerializeField] private RockLevelData _firstRockLevelData;
		
		public int StartingRockCount => _startingRockCount;
		public int HowManyRocksAddPerLevel => _howManyRocksAddPerLevel;
		public IRockLevelData FirstRockLevelData => _firstRockLevelData;
	}
}