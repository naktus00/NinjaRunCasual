using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom Scriptable Objects/Data/Weapons Info Container", fileName = "New Weapons Info Container")]
public class WeaponsInfoContainer : ScriptableObject
{
    public static WeaponsInfoContainer Instance { get { return Resources.Load("Data/WeaponsDataContainer") as WeaponsInfoContainer; } }

    [SerializeField] private Weapon[] _weapons;
    [HideInInspector] public Weapon[] weapons { get { return _weapons; } }

}

[Serializable]
public struct Weapon
{
    [SerializeField] private int _id;
    [SerializeField] private string _weaponName;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Material[] _colorMaterials;

    [HideInInspector] public int id { get { return _id; } }
    [HideInInspector] public string weaponName { get { return _weaponName; } }
    [HideInInspector] public GameObject prefab { get { return _prefab; } }
    [HideInInspector]
    public Material[] colorMaterials
    {
        get
        {
            if (_colorMaterials == null)
                return null;

            if (_colorMaterials.Length <= 0)
                return null;

            return _colorMaterials;
        }
    }
}
