using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gap : MonoBehaviour
{
    bool playerFell = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        if (playerFell == true)
            return;

        playerFell = true;
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        player.FallIntoGap();
        
    }
}
