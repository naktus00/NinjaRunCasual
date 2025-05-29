using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitPoint : MonoBehaviour
{
    private bool killedPlayer;

    private void Awake()
    {
        killedPlayer = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyBoss boss = other.GetComponent<EnemyBoss>();

        if (boss == null)
            return;

        if (killedPlayer == true)
            return;

        killedPlayer = true;
    }
}
