using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    private static VFXManager _instance;
    public static VFXManager Instance { get { return _instance; } }

    [SerializeField] private Dictionary<string, GameObject> _effects;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;

    }
    public void GetEffect()
    {
        //if (_effects[index] == null)
        //    return;

        //GameObject obj = Instantiate
    }


}
