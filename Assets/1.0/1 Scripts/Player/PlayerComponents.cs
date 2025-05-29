using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerComponents : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private Transform _tr;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Collider _coll;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _followTarget;
    [SerializeField] private Transform _rightHandWeaponSlot;
    [SerializeField] private Transform _leftHandWeaponSlot;
    [SerializeField] private Transform _hip;
    [SerializeField] private Transform _axeSlot;
    [SerializeField] private Transform _hammerSlot;
    [SerializeField] private Transform _hammerLine;

    [Space(5)]
    [SerializeField] public GameObject[] weapons;

    [Space(5)]
    [SerializeField] private Transform[] _hitEffectPoints;

    [Space(5)]
    [SerializeField] private NinjaLevelUI _levelUI;

    [HideInInspector] public SkinnedMeshRenderer skinnedMeshRenderer { get { return _skinnedMeshRenderer; } }
    [HideInInspector] public Transform tr { get { return _tr; } }
    [HideInInspector] public Rigidbody rb { get { return _rb; } }
    [HideInInspector] public Collider coll { get { return _coll; } }
    [HideInInspector] public Animator animator { get { return _animator; } }
    [HideInInspector] public Transform followTarget { get { return _followTarget; } }
    [HideInInspector] public Transform rightHandWeaponSlot { get { return _rightHandWeaponSlot; } }
    [HideInInspector] public Transform leftHandWeaponSlot { get { return _leftHandWeaponSlot; } }
    [HideInInspector] public Transform hip { get { return _hip; } }
    [HideInInspector] public Transform axeSlot { get { return _axeSlot; } }
    [HideInInspector] public Transform hammerSlot { get { return _hammerSlot; } }
    [HideInInspector] public Transform hammerLine { get { return _hammerLine; } }
    //[HideInInspector] public GameObject[] weapons { get { return _weapons; } }
    [HideInInspector] public Transform[] hitEffectPoints { get { return _hitEffectPoints; } }
    [HideInInspector] public NinjaLevelUI levelUI { get { return _levelUI; } }

}
