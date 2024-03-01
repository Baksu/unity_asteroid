using Data.Interfaces;

namespace Managers.Interfaces
{
	public interface IDataManager
	{
		public IBaseGameData BaseGameData { get; }
		public IPlayerData PlayerData { get; }
		public IRockData RockData { get; }
	}
}