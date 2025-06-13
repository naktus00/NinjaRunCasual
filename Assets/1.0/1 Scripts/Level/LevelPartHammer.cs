using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPartHammer : LevelPart
{
    public Transform enemiesParent;
    public Transform axeSlotsParent;

    public static float r { get { return 13.5f; } }

    //public List<EnemyStandard> GetEnemies()
    //{
    //    List<EnemyStandard> enemies = new List<EnemyStandard>();
    //    var childs = enemiesParent.GetComponentsInChildren<EnemyStandard>();

    //    foreach (EnemyStandard enemy in childs)
    //    {
    //        enemies.Add(enemy);
    //    }

    //    return enemies;
    //}

}
