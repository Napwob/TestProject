namespace AmayaSoft.ObjectPools
{
    using System.Collections.Generic;
    using UnityEngine;

    public class ObjectPool : MonoBehaviour, IObjectPool<GameObject>
    {
        [SerializeField, Range(1, 15)] private int initialPoolSize;
        [SerializeField] private GameObject prefab;
        private List<GameObject> pool = new List<GameObject>();

        private void Start()
        {
            initializePool();
        }

        private GameObject CreateNewInstance()
        {
            GameObject instance = Instantiate(prefab);
            instance.SetActive(false);
            pool.Add(instance);
            return instance;
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

        public void initializePool()
        {
            for (int i = 0; i < initialPoolSize; i++)
            {
                CreateNewInstance();
            }
        }
    }
}
