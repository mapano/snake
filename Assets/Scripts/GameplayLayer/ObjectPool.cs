using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public BoardElementType type;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    private Dictionary<BoardElementType, Queue<GameObject>> _poolDictionary;

    void Start()
    {
        _poolDictionary = new Dictionary<BoardElementType, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectQueue = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectQueue.Enqueue(obj);
            }

            _poolDictionary[pool.type] = objectQueue;
        }
    }

    public GameObject GetObject(BoardElementType type)
    {
        if (!_poolDictionary.ContainsKey(type))
        {
            Debug.LogError("No pool for type: " + type);
            return null;
        }

        if (_poolDictionary[type].Count == 0)
        {
            Pool pool = pools.Find(p => p.type == type);
            if (pool == null)
            {
                Debug.LogError("No pool definition for type: " + type);
                return null;
            }

            GameObject newObj = Instantiate(pool.prefab);
            newObj.SetActive(true);
            return newObj;
        }

        GameObject obj = _poolDictionary[type].Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObject(BoardElementType type, GameObject obj)
    {
        if (!_poolDictionary.ContainsKey(type))
        {
            Debug.LogError("No pool for type: " + type);
            return;
        }

        obj.SetActive(false);
        _poolDictionary[type].Enqueue(obj);
    }
}