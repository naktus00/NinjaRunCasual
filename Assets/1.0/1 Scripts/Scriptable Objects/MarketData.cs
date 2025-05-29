using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom Scriptable Objects/Data/Market Data", fileName = "New Market Data")]
public class MarketData : ScriptableObject
{
    [Serializable]
    public struct Weapon
    {
        public int id;
        public string weaponName;
        public string purchasedKey;
        public string colorKey;
        public GameObject prefab;
        public Material[] colorMaterials;
        public bool purchased;
        public bool selected;
        public int colorIndex;
    }
    [Serializable]
    public struct Skin
    {
        public int id;
        public string skinName;
        public string purchasedKey;
        public string colorKey;
        public GameObject prefab;
        public Material[] colorMaterials;
        public bool purchased;
        public bool selected;
        public int colorIndex;
    }

    [Header("Market")]
    public Weapon[] Weapons;
}
