using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom Scriptable Objects/Data/Player Data Default", fileName = "New Player Data Default")]
public class PlayerDataDefault : ScriptableObject
{
    [Header("Game")]
    [SerializeField] private int _playerLevel;
    [SerializeField] private int _startingLevel;

    [Space(10)]
    [Header("Currencies")]
    [SerializeField] private int _startingCoin;

    [Space(10)]
    [Header("Custom")]
    [SerializeField] private int _startingWeaponIndex;
    [SerializeField] private int _startingWeaponColor;
    [SerializeField] private int _startingSkinIndex;

    [HideInInspector] public int playerLevel { get { return _playerLevel; } }
    [HideInInspector] public int startingLevel { get { return _startingLevel; } }
    [HideInInspector] public int startingCoin { get { return _startingCoin; } }
    [HideInInspector] public int startingWeaponIndex { get { return _startingWeaponIndex; } }
    [HideInInspector] public int startingWeaponColor { get { return _startingWeaponColor; } }
    [HideInInspector] public int startingSkinIndex { get { return _startingSkinIndex; } }

}
