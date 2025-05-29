using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player == null)
            return;

        player.Jump();
        //Debug.Log("Player Jumping!");
    }
}
