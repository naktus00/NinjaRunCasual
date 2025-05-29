using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom Scriptable Objects/Data/Level Data", fileName = "New Level Data")]
public class LevelData : ScriptableObject
{
    [System.Serializable]
    public struct LevelPartData
    {
        public LevelPart.LevelPartType type;
        public int sample;
        //public float r;
        public List<int> enemiesLevels;
        public bool hasAxe;
        public int axeSlotIndex;

    }

    [Header("GENERAL")]
    public int conceptIndex;    // This idex decide skybox and ground material.
    public int totalCollactableAxeNumber;
    public int rewardCoin;
    public int rewardKilledBossCoin;
    public int rewardPlayerLevel;
    public int totalEnemyNumber
    {
        get
        {
            int n = 0;

            for (int i = 0; i < levelParts.Length; i++)
                n += levelParts[i].enemiesLevels.Count;

            return n;
        }
    }

    [Space(5)]
    public LevelPartData[] levelParts;

}
