using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelPart : MonoBehaviour
{
    public enum LevelPartType
    {
        Empty,
        Straight,
        Zigzag,
        InLine,
        Hammer,
        Jump,
        Boss,
        FakePlayer
    }

    [SerializeField] protected LevelPartType _type;
    [SerializeField] protected int _sample;

    [HideInInspector] public LevelPartType type { get { return _type; } }
    [HideInInspector] public int sample { get { return _sample; } }

    [SerializeField] public Vector3 position;

}
