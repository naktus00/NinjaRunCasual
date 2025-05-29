using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public bool isHitted = false;
    public float damage;

    public void Initialize(float damage)
    {
        isHitted = false;
        this.damage = damage;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}
