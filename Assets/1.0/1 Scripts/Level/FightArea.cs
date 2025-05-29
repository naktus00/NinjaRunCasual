using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FightArea : MonoBehaviour
{
    /*
     * Each hit animatios duration is 1.75s
     * Attack turn duration 1.85f -> 0.50f (it is controlling by time scale)
     * Time Scale 1.00f -> 3.70f
     * Time scale has max value when hit turn 5
     * Time scale increase amount when each turn is 0.54f ()
     * 
     * Players health 100f
     * Attack       = min 5f
     * Def
     * 
     * 
    */

    private Action _onUpdate = null;

    private bool _isFighting = false;
    private float _t0;              // For time loop
    private float _n0;              // Which hit of the players
    private float _nextHitTime;
    private float _waitingTimeToHit;

    [SerializeField] private PlayerController _player;
    [SerializeField] private EnemyFakePlayer _fakePlayer;
    //[SerializeField] private float _minWaitTime;
    [SerializeField] private float _maxTimeScale;

    private void Start()
    {
        _onUpdate = null;
        SubcribeGameActions();
    }

    private void Update()
    {
        if (_onUpdate != null)
            _onUpdate.Invoke();

    }

    private void Initialize()
    {
        _player = GameManager.Instance.player;

        var levelPartFakePlayer = this.gameObject.GetComponent<LevelPartFakePlayer>();
        _fakePlayer = levelPartFakePlayer.fakePlayer;

        _t0 = 0f;
        _n0 = 0f;
        _nextHitTime = 0f;
        _waitingTimeToHit = 1.72f;

        _nextHitTime += _waitingTimeToHit;

        _onUpdate += Fighting;
    }
    private void SubcribeGameActions()
    {
        GameManager.Instance.onArrivedFightArea += Initialize;
        GameManager.Instance.onFightStarted += OnFightStarted;
    }
    private void OnFightStarted()
    {
        _isFighting = true;
    }
    private void Fighting()
    {
        if (_isFighting == false)
            return;

        if (_t0 >= _nextHitTime)
        {
            //Player Hit!
            int n1 = _player.RandomHitAnim();

            float playerDamage = _player.status.damage;
            _fakePlayer.WhenDamageTaken(playerDamage);

            Vector3 pos1 = _fakePlayer.hitEffectPoints[n1].position;
            StartCoroutine(IEHitVFX(n1, pos1, Quaternion.identity, 0.7f));

            //Check Enemy Player Health!
            if (_fakePlayer.status.health <= 0)
            {
                _isFighting = false;

                Vector3 dir = (-1f) * _fakePlayer.transform.forward;
                dir.y += 0.5f;

                _fakePlayer.KilledByPlayer(dir, 1000f, 0.7f);

                Debug.Log("PLAYER Win!");
                return;
            }

            //Enemy Player Hit!
            int n2 = _fakePlayer.RandomHitAnim();

            float enemyPlayerDamage = _fakePlayer.status.damage;
            _player.WhenDamageTaken(enemyPlayerDamage);

            Vector3 pos2 = _player.hitEffectPoints[n2].position;
            StartCoroutine(IEHitVFX(n2, pos2, Quaternion.identity, 0.7f));

            //Check Player Health!
            if (_player.status.health <= 0)
            {
                _isFighting = false;

                Vector3 dir = (-1f) * _player.transform.forward;
                dir.y += 0.5f;

                _player.KilledByFakePlayer(dir, 1000f, 0.7f);

                Debug.Log("FAKE PLAYER Win !");
                return;
            }

            //Set Time Scale
            float timeScale = Time.timeScale;

            if (timeScale <= _maxTimeScale)
            {
                timeScale += 0.05f;
                Time.timeScale = timeScale;
            }

            _nextHitTime += _waitingTimeToHit;
        }

        _t0 += Time.deltaTime;
    }
    private IEnumerator IEHitVFX(int index, Vector3 pos, Quaternion rot, float waitingTime)
    {
        int n = -99;

        switch (index)
        {
            case 0:
                n = 1;
                break;
            case 1:
                n = 7;
                break;
            case 2:
                n = 6;
                break;
            case 3:
                n = 5;
                break;
        }

        yield return new WaitForSeconds(waitingTime);
        var obj = ObjectPool.Instance.InstantiatePooledObj(n, pos, rot);
        ObjectPool.Instance.DestroyPooledObj(obj, 3f);
    }
    
    

}
