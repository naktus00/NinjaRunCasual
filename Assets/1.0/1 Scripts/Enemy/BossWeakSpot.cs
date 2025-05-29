using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossWeakSpot : MonoBehaviour
{
    [SerializeField] public Transform hitPoint;

    private void OnCollisionEnter(Collision collision)
    {
        Axe axe = collision.gameObject.GetComponent<Axe>();

        if (axe == null)
            return;

        if (axe.isHitted)
            return;

        axe.isHitted = true;

        axe.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        DOTween.Kill(axe.gameObject.transform);
        ObjectPool.Instance.DestroyPooledObj(axe.gameObject);

        GameObject sparkEffect = ObjectPool.Instance.InstantiatePooledObj(1, collision.contacts[0].point, Quaternion.identity);
        ObjectPool.Instance.DestroyPooledObj(sparkEffect, 0.75f);

        EnemyBoss boss = gameObject.transform.GetComponentInParent<EnemyBoss>();
        boss.Hitted(axe.damage, this);

        BossComponents bossComps = gameObject.transform.GetComponentInParent<BossComponents>();
        bossComps.animator.SetTrigger("Attacked");

        //Debug.Log("Hit the weak spot by axe");

    }

}
