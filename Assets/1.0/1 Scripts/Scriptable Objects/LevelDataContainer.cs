using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom Scriptable Objects/Data/Level Data Container", fileName = "New Level Data Container")]
public class LevelDataContainer : ScriptableObject
{
    public static LevelDataContainer Instance { get { return Resources.Load("Data/LevelDataContainer") as LevelDataContainer; } }

    [Header("LEVEL PROPS")]
    public GameObject axePrefab;

    [Header("ALL LEVELS DATA")]
    public LevelData[] levelData;
}
