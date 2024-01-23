using Managers.Abstracts;
using UnityEngine;
using UnityEngine.Pool;

namespace Managers
{
	public class BulletsPool : APoolManager<Bullet>
	{
		private IObjectPool<Bullet> _bulletsPool;

		private GameObject _bulletPrefab;
		
		public void Init(GameObject bulletPrefab)
		{
			_bulletPrefab = bulletPrefab;
		}
		
		protected override Bullet CreateObject()
		{
			return Instantiate(_bulletPrefab).GetComponent<Bullet>();
		}
	}
}