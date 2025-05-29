using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHammerPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        player.WhenHammerTaken();
        this.gameObject.SetActive(false);

        //Debug.Log("Hammer Taken !");
    }
}
