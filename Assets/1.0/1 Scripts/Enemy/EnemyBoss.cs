using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public const float AnimationRoaringTime = 5.2f;
    public const float DeathAnimationTime = 2.5f;

    public event Action onHitted;

    [SerializeField] private Transform _killPlayerPoint;
    [SerializeField] private float _speed;
    [SerializeField] private bool _isAlive;
    [SerializeField] private bool _move;

    public float health;

    private BossComponents _comps;

    private void Awake()
    {
        _comps = this.gameObject.GetComponent<BossComponents>();
    }
    private void Start()
    {
        _move = false;
        _isAlive = true;
        _speed = GameManager.Instance.gameData.bossMoveSpeed;

        SubscribeGameActions();
    }
    private void Update()
    {
        if (!_move)
            return;

        MoveToPlayer();

        if (Vector3.Distance(this.gameObject.transform.position, _killPlayerPoint.position) <= 0.1f)
        {
            _move = false;
            PlayerController player = GameManager.Instance.player.GetComponent<PlayerController>();
            player.WhenBossCatches();
            Action action = delegate { StartCoroutine(IEKillPlayer()); };
            StartCoroutine(AddtionalRequirement.WaitTimeAndDo(action, 2f));
            
        }
            
    }
    
    private void SubscribeGameActions()
    {
        GameManager.Instance.onArrivedBoss += () => { StartCoroutine(IEOnArrivedBoss()); };
        GameManager.Instance.onGamePaused += (paused) => { _move = !paused; };
    }
    private float _k0 = 0.001f;
    private void MoveToPlayer()
    {
        transform.Translate(Vector3.forward * _speed * _k0 * Time.deltaTime);
    }
    private void Dead(BossWeakSpot weakSpot)
    {
        _isAlive = false;
        _move = false;

        _comps.coll.enabled = false;
        _comps.rb.isKinematic = true;
        _comps.animator.enabled = false;

        GetComponent<RagdollController>().RagdollON();

        Rigidbody rb = weakSpot.hitPoint.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0f, 1f, 0.5f) * 500f, ForceMode.Impulse);

        StartCoroutine(AddtionalRequirement.DoSlowmotion(0.05f, DeathAnimationTime - 0.5f));

        //Action action = () => { GameManager.Instance.InvokeBossFightOver(true); };
        //StartCoroutine(AddtionalRequirement.WaitTimeAndDo(action, DeathAnimationTime));

        GameManager.Instance.InvokeBossFightOver(true);
    }
    private void SelectEndingDance()
    {
        int n = UnityEngine.Random.Range(0, 3);

        switch (n)
        {
            case 0:
                _comps.animator.SetFloat("DanceValue", 0f);
                break;
            case 1:
                _comps.animator.SetFloat("DanceValue", 0.5f);
                break;
            case 2:
                _comps.animator.SetFloat("DanceValue", 1f);
                break;
        }
    }
    private IEnumerator IEHavePlayerAnyAxe()
    {
        //PlayerController player = GameManager.Instance.player;

        //if (player.collectedAxe > 0)
        //{
        //    UIGameSceneManager.Instance.gameplayPanel.SwitchCrosshair(true);
        //    CameraManager.Instance.AimCam(0.5f);
        //    yield break;
        //}

        //yield return new WaitForSeconds(0.5f);
        //CameraManager.Instance.EndingCam(2f);

        //yield return new WaitForSeconds(2.0f);
        //StartCoroutine(IEKillPlayer());

        PlayerController player = GameManager.Instance.player;

        if (player.collectedAxe > 0)
            yield break;

        yield return new WaitForSeconds(0.5f);
        CameraManager.Instance.EndingCam(1f);

        yield return new WaitForSeconds(2f);
        StartCoroutine(IEKillPlayer());

    }
    private IEnumerator IEOnArrivedBoss()
    {
        _comps.animator.SetTrigger("StartMove");
        yield return new WaitForSeconds(AnimationRoaringTime);

        //_move = true;
        //StartCoroutine(IEHavePlayerAnyAxe());

    }

    public void KillPlayer()
    {
        StartCoroutine(IEKillPlayer());
    }
    private IEnumerator IEKillPlayer()
    {
        PlayerController player = GameManager.Instance.player;

        if (player.isAlive == false)
            yield break;

        _move = false;
        GameInputManager.Instance.DisablePlayerActions();

        while (Vector3.Distance(this.gameObject.transform.position, _killPlayerPoint.position) > 0.3f)
        {
            Vector3 direction = (this.gameObject.transform.position - _killPlayerPoint.position).normalized;
            transform.Translate(direction * 7f * Time.deltaTime);
            yield return null;
        }

        Vector3 rot = _comps.rightHandWeaponSlot.localRotation.eulerAngles;
        rot.z = 0.0f;
        _comps.rightHandWeaponSlot.localRotation = Quaternion.Euler(rot);
        _comps.animator.SetTrigger("StartAttack");

        yield return null;
        _comps.animator.SetBool("Dance", true);
        SelectEndingDance();

        yield return new WaitForSeconds(0.9f);

        player.KilledByBoss();

        GameManager.Instance.InvokeBossFightOver(false, PlayerController.DeathAnimationTime + 2f);

        //yield return new WaitForSeconds(PlayerController.DeathAnimationTime + 2f);

        //GameManager.Instance.InvokeGameEnd(true);
    }

    
    public void Hitted(float damage, BossWeakSpot weakSpot)
    {
        health -= damage;

        if (health <= 0f)
        {
            health = 0f;
            Dead(weakSpot);
        }
        else
        {
            _comps.animator.SetTrigger("Attacked");
        }

        if (_isAlive == true)
            StartCoroutine(IEHavePlayerAnyAxe());

        onHitted?.Invoke();
    }
}
