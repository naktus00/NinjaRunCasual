using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool _instance;
    [HideInInspector] public static ObjectPool Instance { get { return _instance; } }

    [Serializable]
    public struct Pool
    {
        public Queue<GameObject> pooledObjects;
        public GameObject objectPrefab;
        public int poolSize;
    }

    [SerializeField] private Pool[] pools;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        GeneratePools();
    }

    private void GeneratePools()
    {
        for (int i0 = 0; i0 < pools.Length; i0++)
        {
            pools[i0].pooledObjects = new Queue<GameObject>();

            for (int i1 = 0; i1 < pools[i0].poolSize; i1++)
            {
                GameObject obj = Instantiate(pools[i0].objectPrefab);
                obj.SetActive(false);

                obj.transform.SetParent(this.gameObject.transform);

                pools[i0].pooledObjects.Enqueue(obj);
            }
        }
    }

    public GameObject InstantiatePooledObj(int objectType)
    {
        GameObject obj = pools[objectType].pooledObjects.Dequeue();

        if (obj == null)
            return null;

        Vector3 objPosition = Vector3.zero;
        obj.transform.position = objPosition;

        Quaternion objRotation = Quaternion.identity;
        obj.transform.rotation = objRotation;

        pools[objectType].pooledObjects.Enqueue(obj);
        obj.SetActive(true);
        return obj;

    }
    public GameObject InstantiatePooledObj(int objectType, Vector3 position, Quaternion rotation)
    {
        GameObject obj = pools[objectType].pooledObjects.Dequeue();

        if (obj == null)
            return null;

        Vector3 objPosition = position;
        obj.transform.position = objPosition;

        Quaternion objRotation = rotation;
        obj.transform.rotation = objRotation;

        pools[objectType].pooledObjects.Enqueue(obj);
        obj.SetActive(true);
        return obj;

    }
    public GameObject InstantiatePooledObj(int objectType, Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject obj = pools[objectType].pooledObjects.Dequeue();

        if (obj == null)
            return null;

        Vector3 objPosition = position;
        obj.transform.position = objPosition;

        Quaternion objRotation = rotation;
        obj.transform.rotation = objRotation;

        obj.transform.SetParent(parent);

        pools[objectType].pooledObjects.Enqueue(obj);
        obj.SetActive(true);
        return obj;

    }
    public void DestroyPooledObj(GameObject obj)
    {
        if (obj == null)
            return;

        obj.SetActive(false);

        Vector3 objPosition = Vector3.zero;
        obj.transform.position = objPosition;

        Quaternion objRotation = Quaternion.identity;
        obj.transform.rotation = objRotation;

        obj.transform.SetParent(this.gameObject.transform);

    }
    public void DestroyPooledObj(GameObject obj, float time)
    {
        Action<GameObject> action = null;

        action = (gameObject) =>
        {
            if (gameObject == null)
            {
                //Debug.Log("Obj NULL!");
                return;
            }

            gameObject.SetActive(false);

            Vector3 objPosition = Vector3.zero;
            gameObject.transform.position = objPosition;

            Quaternion objRotation = Quaternion.identity;
            gameObject.transform.rotation = objRotation;

            gameObject.transform.SetParent(this.gameObject.transform);
            //Debug.Log("Pooled Object disactive and returned to the Parent!");
        };

        //action = delegate
        //{
        //    if (obj == null)
        //    {
        //        Debug.Log("Obj NULL!");
        //        return;
        //    }

        //    obj.SetActive(false);

        //    Vector3 objPosition = Vector3.zero;
        //    obj.transform.position = objPosition;

        //    Quaternion objRotation = Quaternion.identity;
        //    obj.transform.rotation = objRotation;

        //    obj.transform.SetParent(this.gameObject.transform);
        //    Debug.Log("Pooled Object disactive and returned to the Parent!");
        //};

        StartCoroutine(AddtionalRequirement.WaitTimeAndDo(() => action(obj), time));
        
    }
}
