using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom Scriptable Objects/Data/Level Parts Prefab Container", fileName = "New Level Parts Prefab Container")]
public class LevelPartsPrefabContainer : ScriptableObject
{
    public static LevelPartsPrefabContainer Instance { get { return Resources.Load("Data/LevelPartsPrefabContainer") as LevelPartsPrefabContainer; } }

    [Space(10), Header("Level Prefabs")]
    public GameObject[] LevelPartEmptyPrefabs;
    public GameObject[] LevelPartStraightPrefabs;
    public GameObject[] LevelPartZigzagPrefabs;
    public GameObject[] LevelPartInLinePrefabs;
    public GameObject[] LevelPartHammerPrefabs;
    public GameObject[] LevelPartJumpPrefabs;
    public GameObject LevelPartBossPrefabs;
    public GameObject levelPartFakePlayer;
}
