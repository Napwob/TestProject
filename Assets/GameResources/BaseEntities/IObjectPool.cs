using UnityEngine;

public interface IObjectPool<T> where T : class
{
    T GetPooledObject();
}