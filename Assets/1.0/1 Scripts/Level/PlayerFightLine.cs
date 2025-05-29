using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFightLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.InvokeArrivedFightArea();
            Debug.Log("layer arrived fight line!");
        }
    }
}
