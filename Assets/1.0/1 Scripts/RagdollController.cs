using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [SerializeField] private GameObject[] _ragdollBits;
    [HideInInspector] public GameObject[] ragdollBits { get { return _ragdollBits; } }

    private void Start()
    {
        RagdollOFF();
    }

    public void RagdollON()
    {
        foreach (GameObject ragdollObj in _ragdollBits)
        {
            ragdollObj.layer = 17;
            ragdollObj.GetComponent<Collider>().enabled = true;
            ragdollObj.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    public void RagdollOFF()
    {
        foreach (GameObject ragdollObj in _ragdollBits)
        {
            ragdollObj.layer = 0;
            ragdollObj.GetComponent<Collider>().enabled = false;
            ragdollObj.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
