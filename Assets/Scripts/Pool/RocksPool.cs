using Enemies;
using Pool.Abstract;
using UnityEngine;
using UnityEngine.Pool;

namespace Pool
{
	public class RocksPool : APoolObject<Rock>
	{
		private IObjectPool<Rock> _bulletsPool;

		private readonly GameObject _rockPrefab;
		
		public RocksPool(GameObject rockPrefab)
		{
			_rockPrefab = rockPrefab;
		}
		
		protected override Rock CreateObject()
		{
			return Object.Instantiate(_rockPrefab).GetComponent<Rock>();
		}
	}
}