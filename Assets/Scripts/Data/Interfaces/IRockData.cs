namespace Data.Interfaces
{
	public interface IRockData
	{
		public int StartingRockCount { get; }
		public int RockSpawnAfterDestroy { get; }
		public int HowManyRocksAddPerLevel { get; }
		public IRockLevelData FirstRockLevelData { get; }
	}
}