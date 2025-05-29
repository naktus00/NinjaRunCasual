using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom Scriptable Objects/Data/Game Data", fileName = "New Game Data")]
public class GameData : ScriptableObject
{
    [Header("Game Features")]
    [Tooltip("This value affects the running speed of the player on the platform.")]
    public float runSpeed;
    [Tooltip("This value affects the aim speed of player when its using axe.")]
    public float aimSpeed;
    public float aimFocusingTime;
    [Tooltip("This value affects the speed of the player's movement to the right and left on the platform.")]
    public float moveSensivity;
    public float playerMinPosOnPlatform;
    public float playerMaxPosOnPlatform;
    public float axeDamage;
    public float axeThrowPower;
    public float bossMoveSpeed;

}
