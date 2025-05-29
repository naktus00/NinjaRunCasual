using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom Scriptable Objects/Data/Skin Info Container", fileName = "New Skin Info Container")]
public class SkinInfoContainer : ScriptableObject
{
    public static SkinInfoContainer Instance { get { return Resources.Load("Data/SkinInfoContainer") as SkinInfoContainer; } }

    [SerializeField] private Skin[] _skins;
    [HideInInspector] public Skin[] skins { get { return _skins; } }

}

[Serializable]
public struct Skin
{
    [SerializeField] private int _id;
    [SerializeField] private string _skinName;
    [SerializeField] private Vector2 _tilingValues;
    [SerializeField] private Vector2 _offsetValues;

    [HideInInspector] public int id { get { return _id; } }
    [HideInInspector] public string skinName { get { return _skinName; } }
    [HideInInspector] public Vector2 tilingValues { get { return _tilingValues; } }
    [HideInInspector] public Vector2 offsetValues { get { return _offsetValues; } }
}
