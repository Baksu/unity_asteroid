using System;
using Data.Interfaces;
using UnityEngine;

namespace Data
{
	[CreateAssetMenu(fileName = "New bullet data", menuName = "Data/Bullet", order = 1)]
	[Serializable]
	public class BulletData : ScriptableObject, IBulletData
	{
		[SerializeField] private GameObject _baseBulletPrefab;
		[SerializeField] private float _bulletSpeed;
		[SerializeField] private int _bulletLifeTimeInMS;

		public GameObject BaseBulletPrefab => _baseBulletPrefab;
		public float BulletSpeed => _bulletSpeed;
		public int BulletLifeTimeInMS => _bulletLifeTimeInMS;
	}
}