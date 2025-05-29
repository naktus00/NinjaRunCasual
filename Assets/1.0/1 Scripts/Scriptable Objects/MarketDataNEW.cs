using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom Scriptable Objects/Data/Market Data NEWW", fileName = "New Market Data NEWW")]
public class MarketDataNEW : ScriptableObject
{
    [SerializeField] private TradeWeapon[] _weapons;
    [HideInInspector] public TradeWeapon[] weapons { get { return _weapons; } }

    [Space(10)]
    [SerializeField] private TradeSkin[] _skins;
    [HideInInspector] public TradeSkin[] skins { get { return _skins; } }
}

[Serializable]
public struct TradeWeapon
{
    [SerializeField] private int _weaponID;
    [SerializeField] private string _weaponName;
    [SerializeField] private int _weaponCost;
    [SerializeField] private int[] _colorsCosts;

    [HideInInspector] public int weaponID { get { return _weaponID; } }
    [HideInInspector] public string weaponName { get { return _weaponName; } }
    [HideInInspector] public int weaponCost { get { return _weaponCost; } }
    [HideInInspector] public int[] colorsCosts { get { return _colorsCosts; } }
    [HideInInspector] public int colorNumber 
    { 
        get 
        {
            if (_colorsCosts == null)
                return 0;

            return _colorsCosts.Length;
        } 
    }

}

[Serializable]
public struct TradeSkin
{
    [SerializeField] private int _skinID;
    [SerializeField] private string _skinName;
    [SerializeField] private int _cost;

    [HideInInspector] public int skinID { get { return _skinID; } }
    [HideInInspector] public string skinName { get { return _skinName; } }
    [HideInInspector] public int cost { get { return _cost; } }

}
