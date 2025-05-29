using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightLine : MonoBehaviour
{
    [SerializeField] private bool playerArrived;

    private void Awake()
    {
        playerArrived = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerArrived == true)
            return;

        if (other.tag != "Player")
            return;

        playerArrived = true;
        GameManager.Instance.InvokeArrivedBoss();
    }
}
