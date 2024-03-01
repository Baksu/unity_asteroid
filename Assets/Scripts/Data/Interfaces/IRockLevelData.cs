using UnityEngine;

namespace Data.Interfaces
{
	public interface IRockLevelData
	{
		public GameObject Prefab { get; }
		public float MinSpeed { get; }
		public float MaxSpeed { get; }
		public IRockLevelData NextLevel { get; }
		public int PointsForDestroy { get; }
	}
}