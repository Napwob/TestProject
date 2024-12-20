namespace AmayaSoft.ObjectPools
{
	using System.Collections.Generic;
	using UnityEngine;

	public abstract class ObjectPoolBase : MonoBehaviour
	{
		[Header("Pool Settings")]
		[SerializeField, Range(1, 15)] private int initialPoolSize;
		private List<GameObject> pool = new List<GameObject>();

		protected abstract GameObject CreateNewInstance();

		protected virtual void InitializePool()
		{
			for (int i = 0; i < initialPoolSize; i++)
			{
				CreateNewInstance();
			}
		}

		public GameObject GetPooledObject()
		{
			foreach (GameObject obj in pool)
			{
				if (!obj.activeInHierarchy)
				{
					return obj;
				}
			}
			return CreateNewInstance();
		}

		protected void AddToPool(GameObject instance)
		{
			instance.SetActive(false);
			pool.Add(instance);
		}

		protected List<GameObject> GetPool() => pool;

		private void Start()
		{
			InitializePool();
		}
	}
}