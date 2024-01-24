using DefaultNamespace.Interfaces;
using Managers.Interfaces;
using UnityEngine;
using UnityEngine.Pool;

namespace Managers.Abstracts
{
	//We also can use this for rocks
	public abstract class APoolObject<T> : MonoBehaviour, IPoolManager<T> where T : class, IPoolObject
	{
		private IObjectPool<T> _pool;

		private IObjectPool<T> Pool
		{
			get
			{
				if (_pool == null)
				{
					_pool = new LinkedPool<T>(CreateObject);
				}

				return _pool;
			}
		}

		protected abstract T CreateObject();
		
		public T GetObject()
		{
			var poolObject = Pool.Get();
			poolObject.AfterGet();
			return poolObject;
		}

		public void ReleaseObject(T poolObject)
		{
			poolObject.BeforeRelease();
			Pool.Release(poolObject);
		}
	}
}