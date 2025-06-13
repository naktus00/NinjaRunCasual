using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlayerComponents : MonoBehaviour
{
    [SerializeField] private Transform _tr;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Collider _coll;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _followTarget;
    [SerializeField] private Transform _rightHandWeaponSlot;
    [SerializeField] private Transform _leftHandWeaponSlot;
    [SerializeField] public GameObject[] _weapons;
    [SerializeField] private Transform[] _hitEffectPoints;

    [HideInInspector] public Transform tr { get { return _tr; } }
    [HideInInspector] public Rigidbody rb { get { return _rb; } }
    [HideInInspector] public Collider coll { get { return _coll; } }
    [HideInInspector] public Animator animator { get { return _animator; } }
    [HideInInspector] public Transform followTarget { get { return _followTarget; } }
    [HideInInspector] public Transform rightHandWeaponSlot { get { return _rightHandWeaponSlot; } }
    [HideInInspector] public Transform leftHandWeaponSlot { get { return _leftHandWeaponSlot; } }
    [HideInInspector] public GameObject[] weapons { get { return _weapons; } }
    [HideInInspector] public Transform[] hitEffectPoints { get { return _hitEffectPoints; } }
}
