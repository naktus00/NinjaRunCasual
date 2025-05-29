using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType { Standard, BigOne, CanBeKilledByAxe}

    [SerializeField] private EnemyType _type;
    [HideInInspector] public EnemyType Type { get { return _type; } }

    private EnemyComponents comps;

    private void Start()
    {
        switch (_type)
        {
            case EnemyType.Standard:
                break;
            case EnemyType.BigOne:
                break;
            case EnemyType.CanBeKilledByAxe:
                break;
        }
    }

    public void ContactPlayer()
    {

    }
}
