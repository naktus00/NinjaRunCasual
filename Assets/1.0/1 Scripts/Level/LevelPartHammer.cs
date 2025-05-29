using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPartHammer : LevelPart
{
    public Transform enemiesParent;
    public Transform axeSlotsParent;

    public static float r { get { return 13.5f; } }

    //public LevelPartHammer(int sample, Vector3 position)
    //{
    //    _type = LevelPartType.Hammer;
    //    _sample = sample;
    //    _position = position;

    //}

    public List<EnemyStandard> GetEnemies()
    {
        List<EnemyStandard> enemies = new List<EnemyStandard>();
        var childs = enemiesParent.GetComponentsInChildren<EnemyStandard>();

        foreach (EnemyStandard enemy in childs)
        {
            enemies.Add(enemy);
        }

        return enemies;
    }

    //public void SetHammerPosition()
    //{
    //    float x = -99f;
    //    float y = 0f;
    //    float z = -9f;

    //    int n = Random.Range(0, 3);

    //    switch (n)
    //    {
    //        case 0:
    //            x = -2f;
    //            break;
    //        case 1:
    //            x = 0f;
    //            break;
    //        case 2:
    //            x = 2f;
    //            break;
    //    }

    //    hammerPosition = new Vector3(x, y, z);
    //    hammerPoint.localPosition = hammerPosition;
    //}

}
