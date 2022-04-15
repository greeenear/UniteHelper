using System;
using System.Collections.Generic;
using UnityEngine;

// use objectPool.OnCreateRequest += ()=> (prefabComponent)Instantiate(prefab);

public class ObjectPool<T> where T: UnityEngine.Component
{
    public event Func<T> OnCreateRequest;
    private Stack<T> pool;

    public ObjectPool(int startSize)
    {
        pool = new Stack<T>(startSize);
    }

    public T Get()
    {
        if (pool.Count > 0)
        {
            var curObject = pool.Pop();
            curObject.gameObject.SetActive(true);
            return curObject;
        }

        return OnCreateRequest?.Invoke();
    }

    public void Return(T returnedObject)
    {
        if (returnedObject == null)
        {
            Debug.LogError("ObjectPull: entity is null");
            return;
        }

        pool.Push(returnedObject);
        returnedObject.gameObject.SetActive(false);
    }

    public void Remove(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (pool.Count <= 0)
            {
                return;
            }

            GameObject.Destroy(pool.Pop().gameObject);
        }
    }

    public int GetSize()
    {
        return pool.Count;
    }
}