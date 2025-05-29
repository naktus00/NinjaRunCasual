using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComponents : MonoBehaviour
{

    [SerializeField] private Transform _tr;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Collider _coll;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _rightHandWeaponSlot;
    [SerializeField] private Transform _leftHandWeaponSlot;
    [SerializeField] private Transform _hip;
    [SerializeField] private Transform _levelCanvasParent;
    [SerializeField] private NinjaLevelUI _enemyLevelUI;

    [HideInInspector] public Transform tr { get { return _tr; } }
    [HideInInspector] public Rigidbody rb { get { return _rb; } }
    [HideInInspector] public Collider coll { get { return _coll; } }
    [HideInInspector] public Animator animator { get { return _animator; } }
    [HideInInspector] public Transform rightHandWeaponSlot { get { return _rightHandWeaponSlot; } }
    [HideInInspector] public Transform leftHandWeaponSlot { get { return _leftHandWeaponSlot; } }
    [HideInInspector] public Transform hip { get { return _hip; } }
    [HideInInspector] public Transform levelCanvasParent { get { return _levelCanvasParent; } }
    [HideInInspector] public NinjaLevelUI enemyLevelUI { get { return _enemyLevelUI; } }

}
