using UnityEngine;

namespace Data.Interfaces
{
	public interface IBaseGameData
	{
		public int StartLives { get; }
		public GameObject BaseBulletPrefab { get; }
	}
}