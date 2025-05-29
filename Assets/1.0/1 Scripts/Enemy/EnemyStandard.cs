using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStandard : MonoBehaviour
{
    [HideInInspector] private EnemyComponents _comps;
    [SerializeField] private float _force;
    [SerializeField] private float _attackAmount;

    [SerializeField] private int _level;
    [HideInInspector] public int level { get { return _level; } }
    [HideInInspector] public float attackAmount { get { return _attackAmount; } }

    private void Awake()
    {
        _comps = GetComponent<EnemyComponents>();
    }

    private void Start()
    {
        _comps.levelCanvasParent.LookAt(CameraManager.Instance.mainCam.transform);
    }

    //private void Update()
    //{
    //    comps.levelCanvasParent.LookAt(CameraManager.Instance.mainCam.transform);
    //}

    public void Initialize(int level)
    {
        this._level = level;
        _comps.enemyLevelUI.SetLevel(level);
        _comps.levelCanvasParent.LookAt(CameraManager.Instance.mainCam.transform);
    }
    public void KilledByStandardHit()
    {
        _comps.coll.enabled = false;
        _comps.rb.isKinematic = true;
        _comps.animator.enabled = false;
        _comps.levelCanvasParent.gameObject.SetActive(false);

        GetComponent<RagdollController>().RagdollON();

        _comps.hip.GetComponent<Rigidbody>().AddForce(Vector3.up * _force, ForceMode.Impulse);

        Destroy(this.gameObject, 1f);
    }
    public void KilledByHammer()
    {
        _comps.coll.enabled = false;
        _comps.rb.isKinematic = true;
        _comps.animator.enabled = false;
        _comps.levelCanvasParent.gameObject.SetActive(false);

        GetComponent<RagdollController>().RagdollON();

        float dirX = UnityEngine.Random.Range(-0.5f, 0.5f);
        float dirY = 1f;
        float dirZ = 0f;

        Vector3 dir = new Vector3(dirX, dirY, dirZ).normalized;

        _comps.hip.GetComponent<Rigidbody>().AddForce(dir * 1250f, ForceMode.Impulse);

        float t = UnityEngine.Random.Range(1f, 1.2f);
        Destroy(this.gameObject, t);
    }
    public void InteractLowLevelPlayer()
    {
        //TO DO: Vurma animasyonu aktif et.
        //TO DO: Vurma ses efekti eklenebilir.
        _comps.rb.isKinematic = true;
    }
    
}
