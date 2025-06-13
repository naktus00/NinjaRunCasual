using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom Scriptable Objects/Data/Map Data", fileName = "New Map Data")]
public class MapData : ScriptableObject
{
    [Serializable]
    public struct MapArea
    {
        public string name;
        public int index;
        public float mainProgress;
        public MapArea_Structer[] structers;
    }

    [Serializable]
    public struct MapArea_Structer
    {
        public int index;
        public int requirement;
        public float progress;
    }

    public MapArea[] mapAreas;
}
