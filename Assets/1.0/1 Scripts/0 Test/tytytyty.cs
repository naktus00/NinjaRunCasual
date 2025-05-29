using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class tytytyty : MonoBehaviour
{
    [Serializable]
    public struct TewstStruct
    {
        public int n1;
        public float f1;
    }

    [SerializeField] private int testN;
    [SerializeField] private float testF;
    [SerializeField] private TewstStruct str;

    private void Start()
    {
        str = new TewstStruct();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            RRR(str);
            Debug.Log("Done!");
        }
    }

    public void RRR(TewstStruct str)
    {
        str.n1 = testN;
        str.f1 = testF;
    }
}
