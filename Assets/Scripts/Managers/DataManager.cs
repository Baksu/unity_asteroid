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
		[SerializeField] private BulletData _bulletData;
		[SerializeField] private RockData _rockData;

		public IBaseGameData BaseGameData => _baseGameData;
		public IPlayerData PlayerData => _playerData;
		public IBulletData BulletData => _bulletData;
		public IRockData RockData => _rockData;
	}
}