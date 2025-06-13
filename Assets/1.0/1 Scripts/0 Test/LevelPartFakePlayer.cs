using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPartFakePlayer : LevelPart
{
    private static float _r = 12.0f;
    public static float r { get { return _r; } }

    [SerializeField] private Transform _fightCamLocTr;
    [SerializeField] private Transform _playerWinCamLocTr;
    [SerializeField] private Transform _fakePlayerWinCamLocTr;
    [SerializeField] private Transform _playerLocTr;

    [Space(5)]
    [SerializeField] private EnemyFakePlayer _fakePlayer;

    [HideInInspector] public Transform fightCamLocTr { get { return _fightCamLocTr; } }
    [HideInInspector] public Transform playerWinCamLocTr { get { return _playerWinCamLocTr; } }
    [HideInInspector] public Transform fakePlayerWinCamLocTr { get { return _fakePlayerWinCamLocTr; } }
    [HideInInspector] public Transform playerLocTr { get { return _playerLocTr; } }
    [HideInInspector] public EnemyFakePlayer fakePlayer { get { return _fakePlayer; } }

    
}
