using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HammerAimLine : MonoBehaviour
{
    private List<EnemyStandard> enemies;

    private void OnEnable()
    {
        enemies = new List<EnemyStandard>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy")
            return;

        EnemyStandard enemy = other.gameObject.GetComponent<EnemyStandard>();

        if(enemies.Contains(enemy) == false)
            enemies.Add(enemy);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Enemy")
            return;

        EnemyStandard enemy = other.gameObject.GetComponent<EnemyStandard>();

        if (enemies.Contains(enemy) == true)
            enemies.Remove(enemy);
    }

    public void KillEnemies(ref int killedEnemies, ref int playerLevel)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].KilledByHammer();
            playerLevel++;
            killedEnemies++;
        }
    }


}
