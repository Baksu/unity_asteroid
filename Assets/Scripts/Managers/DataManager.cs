using Data;
using Data.Interfaces;
using Managers.Interfaces;
using UnityEngine;

namespace Managers
{
	public class DataManager : MonoBehaviour, IDataManager
	{
		[SerializeField] private BaseGameData _baseGameData;
		[SerializeField] private PlayerData _playerData;
		[SerializeField] private RockData _rockData;

		public IBaseGameData BaseGameData => _baseGameData;
		public IPlayerData PlayerData => _playerData;
		public IRockData RockData => _rockData;
	}
}