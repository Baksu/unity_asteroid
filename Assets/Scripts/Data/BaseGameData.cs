using System;
using UnityEngine;

namespace Data
{
	[CreateAssetMenu(fileName = "New game data", menuName = "Data/Game data", order = 1)]
	[Serializable]
	public class BaseGameData : ScriptableObject
	{
		[SerializeField] private int _startLives;
		[SerializeField] private GameObject _baseBullet;
		
		public int StartLives => _startLives;
		public GameObject BaseBullet => _baseBullet;
	}
}