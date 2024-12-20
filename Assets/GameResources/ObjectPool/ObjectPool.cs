namespace AmayaSoft.ObjectPools
{
	using UnityEngine;
	public class ObjectPool : ObjectPoolBase
	{
		[SerializeField] private GameObject prefab;

		protected override GameObject CreateNewInstance()
		{
			GameObject instance = Instantiate(prefab);
			AddToPool(instance);
			return instance;
		}
	}
}
