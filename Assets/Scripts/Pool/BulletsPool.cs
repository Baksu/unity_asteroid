using Player;
using Pool.Abstract;
using UnityEngine;
using UnityEngine.Pool;

namespace Pool
{
	public class BulletsPool : APoolObject<Bullet>
	{
		private IObjectPool<Bullet> _bulletsPool;

		private readonly GameObject _bulletPrefab;
		
		public BulletsPool(GameObject bulletPrefab)
		{
			_bulletPrefab = bulletPrefab;
		}
		
		protected override Bullet CreateObject()
		{
			return Object.Instantiate(_bulletPrefab).GetComponent<Bullet>();
		}
	}
}