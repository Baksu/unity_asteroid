using UnityEngine;

namespace Data.Interfaces
{
	public interface IPlayerData
	{
		public GameObject PlayerShipPrefab { get; }
		public float RotationSpeed { get; }
		public float Thrust { get; }
		public float Friction { get; }
		public float MinimumVelocityToMove { get; }
		public int IndestructibleAfterSpawnInMS { get; }
	}
}