using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAxePoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        player.PickUpAxe();
        this.gameObject.SetActive(false);

        player.InvokeOnPickUpAxe();

        //Debug.Log("Axe Taken !");
    }
}
