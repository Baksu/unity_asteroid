using System;
using Data.Interfaces;
using UnityEngine;

namespace Data
{
	[CreateAssetMenu(fileName = "New game data", menuName = "Data/Game data", order = 1)]
	[Serializable]
	public class BaseGameData : ScriptableObject, IBaseGameData
	{
		[SerializeField] private int _startLives;
		
		public int StartLives => _startLives;
	}
}