using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom Scriptable Objects/Data/Player Data", fileName = "New Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("CURRENCIES")]
    public int coin;
    public int diamond;

    [Space(10), Header("PLAYER FEATURES")]
    public int health;
    public int power;

    [Space(3), Header("Status")]
    public int hp;
    public int atk;
    public int def;

    [Space(3), Header("Weapon")]
    public int selectedWeaponIndex;
    public int selectedWeaponColorIndex;

    [Space(3), Header("Skin")]
    public int selectedSkinIndex;

    [Space(3), Header("Progress")]
    public int progressValue;
    public int currentMapIndex;

    [Space(3), Header("Player Rank")]
    public int score;
    public int leagueIndex;
    public int rank;

}
