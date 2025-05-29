using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffectManager : MonoBehaviour
{
    private static VisualEffectManager _instance;
    public static VisualEffectManager instance { get { return _instance; } }

    [SerializeField] private GameObject[] particalEffects;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;

    }
    public void InstantiateParticalEffect(int index, Vector3 position, Vector3 rotation, Transform parent, float duration)
    {
        Transform newObjParent;

        if (parent == null)
            newObjParent = this.gameObject.transform;
        else
            newObjParent = parent;

        Quaternion rot = Quaternion.Euler(rotation);

        GameObject particalEffect = Instantiate(particalEffects[index], position, rot, newObjParent);

        Destroy(particalEffect, duration);
    }

}
