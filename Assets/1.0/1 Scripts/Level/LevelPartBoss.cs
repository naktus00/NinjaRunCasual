using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPartBoss : LevelPart
{
    public Transform bossTr;

    private static float _r = 14f;
    public static float r { get { return _r; } }

    //public LevelPartBoss(int sample, Vector3 position)
    //{
    //    _type = LevelPartType.Boss;
    //    _sample = sample;
    //    _position = position;

    //}
}
