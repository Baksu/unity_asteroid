using UnityEngine;

namespace Data.Interfaces
{
	public interface IBulletData
	{
		public GameObject BaseBulletPrefab { get; }
		public float BulletSpeed { get; }
		public int BulletLifeTimeInMS { get; }
	}
}