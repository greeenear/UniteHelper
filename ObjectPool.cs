using System;
using System.Collections.Generic;
using UnityEngine;

// use objectPool.OnCreateRequest += ()=> (prefabComponent)Instantiate(prefab);

public class ObjectPool<T> where T: UnityEngine.Component
{
    public event Func<T> OnCreateRequest;
    private Stack<T> pull;

    public ObjectPool(int startSize)
    {
        pull = new Stack<T>(startSize);
    }

    public T Get()
    {
        if (pull.Count > 0)
        {
            var curObject = pull.Pop();
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

        pull.Push(returnedObject);
        returnedObject.gameObject.SetActive(false);
    }

    public void Remove(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (pull.Count <= 0)
            {
                return;
            }

            GameObject.Destroy(pull.Pop());
        }
    }

    public int GetSize()
    {
        return pull.Count;
    }
}