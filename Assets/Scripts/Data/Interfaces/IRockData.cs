namespace Data.Interfaces
{
	public interface IRockData
	{
		public int StartingRockCount { get; }
		public int HowManyRocksAddPerLevel { get; }
		public IRockLevelData FirstRockLevelData { get; }
	}
}