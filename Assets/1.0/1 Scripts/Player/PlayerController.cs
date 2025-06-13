using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    public const float DeathAnimationTime = 3f;
    private PlayerComponents _playerComponents;

    private Action OnPlayerUpdate;

    public event Action onKilledEnemy;
    public event Action onPickUpAxe;

    private float _moveSensivity;
    private float _runSpeed;
    private float _aimSpeed;
    private float _aimFocusingTime;
    private float _jumpPower = 750f;
    private float _playerMinPosOnPlatform;
    private float _playerMaxPosOnPlatform;
    private float _axeDamage;
    private float _axeThrowPower;
    private float _invulnerableTime;

    private float _angleH;
    private float _angleV;
    private LayerMask _layerMask;

    public bool isAlive;
    public bool isOnTheGround;
    public bool canPlayerRun;
    public bool canPlayerMoveLeftRight;
    public bool hitting;
    public bool aimON;
    public bool isReadyToFight;
    public int collectedAxe;

    [Header("Player")]
    [SerializeField] private int _level;
    [SerializeField] private int _killedEnemyNumber;
    [SerializeField] private float _health;

    [HideInInspector] public int level { get { return _level; } }
    [HideInInspector] public int killedEnemyNumber { get { return _killedEnemyNumber; } }


    //------
    [SerializeField] private NinjaWarriorStatus _status;
    //------

    //------
    [HideInInspector] public NinjaWarriorStatus status { get { return _status; } }
    [HideInInspector] public Transform[] hitEffectPoints { get { return _playerComponents.hitEffectPoints; } }
    //------

    private void Awake()
    {
        OnPlayerUpdate = null;
        _layerMask = LayerMask.GetMask("Aimable");
    }

    private void Start()
    {
        InitializeVariables();
        SubscribeInputActions();
        SubscribeGameActions();

        _playerComponents.rb.isKinematic = true;

        _playerComponents.levelUI.transform.LookAt(Camera.main.transform);
        _playerComponents.levelUI.SetLevel(_level);
    }

    private void Update()
    {
        if (OnPlayerUpdate != null)
            OnPlayerUpdate.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyStandard enemy = collision.gameObject.GetComponent<EnemyStandard>();

            if(_level >= enemy.level)
            {
                onKilledEnemy?.Invoke();
                enemy.KilledByStandardHit();
                NinjaLevelUI.CheckAllEnemyUI();

                //Debug.Log("Player level UPPER than the enemy level");
            }
            else
            {
                enemy.InteractLowLevelPlayer();
                KilledByEnemyStandard();

                //Debug.Log("Player level LOWER than the enemy level");
            }
        }

        else if (collision.gameObject.tag == "Ground")
        {
            if (isOnTheGround == true)
                return;

            isOnTheGround = true;
            canPlayerMoveLeftRight = true;
            _playerComponents.rb.useGravity = false;
            _playerComponents.animator.SetBool("jump", false);
            
        }

    }
    private void InitializeVariables()
    {
        var gameData = GameManager.Instance.gameData;
        _moveSensivity = gameData.moveSensivity;
        _runSpeed = gameData.runSpeed;
        _aimSpeed = gameData.aimSpeed;
        _aimFocusingTime = gameData.aimFocusingTime;
        _playerMinPosOnPlatform = gameData.playerMinPosOnPlatform;
        _playerMaxPosOnPlatform = gameData.playerMaxPosOnPlatform;
        _axeDamage = gameData.axeDamage;
        _axeThrowPower = gameData.axeThrowPower;

        _playerComponents = this.gameObject.GetComponent<PlayerComponents>();

        isAlive = true;
        isOnTheGround = true;
        canPlayerRun = false;
        canPlayerMoveLeftRight = false;
        hitting = false;
        aimON = false;
        isReadyToFight = false;

        collectedAxe = 0;

        var user = DataManager.Instance.user;
        _level = user.playerLevel;

        //NinjaWarriorStatus.Initialize(ref _status);
    }
    private void SubscribeInputActions()
    {
        GameInputManager.Instance.playerMovementXAxis += PlayerMoveLeftRight;
        GameInputManager.Instance.axeAim += PlayerAim;
    }
    private void SubscribeGameActions()
    {
        onKilledEnemy += KillEnemyStandard;

        GameManager.Instance.onGameStarted += OnGameStarted;
        GameManager.Instance.onGameRunning += PlayerRunOnPlatform;
        GameManager.Instance.onGamePaused += (paused) =>
        {
            if (paused)
                canPlayerRun = false;
            else
                canPlayerRun = true;
        };

        GameManager.Instance.onArrivedBoss += () => { StartCoroutine(IEOnArrivedBoss()); };
        GameManager.Instance.onBossFightStarted += () => { StartCoroutine(IEOnStartedBossFight()); };
        GameManager.Instance.onBossFightOver += (playerWin) =>
        {
            if(playerWin)
                StartCoroutine(IEOnBossDead()); 
        };


        ////-------------
        //GameManager.Instance.onArrivedFightArea += () => { StartCoroutine(IEOnArrivedFightArea()); };
        //GameManager.Instance.onFightEnded += (b) => {

        //    if (b == false)
        //        return;

        //    StartCoroutine(IEOnFightEnded());
        //};
        ////-------------
    }
    private void PlayerRunOnPlatform()
    {
        if (canPlayerRun == false)
            return;

        float newZ = transform.position.z + _runSpeed * Time.deltaTime;
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, newZ);
        transform.position = newPosition;
    }
    private void PlayerMoveLeftRight(Vector2 delta)
    {
        if (canPlayerMoveLeftRight == false)
            return;

        if (delta.x == 0.0f)
            return;

        float newX = transform.position.x + delta.x * _moveSensivity * Time.deltaTime;
        newX = Mathf.Clamp(newX, _playerMinPosOnPlatform, _playerMaxPosOnPlatform);
        Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z);
        transform.position = newPosition;

    }
    public void Jump()
    {
        canPlayerMoveLeftRight = false;
        isOnTheGround = false;
        _playerComponents.rb.useGravity = true;
        _playerComponents.rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        _playerComponents.animator.SetBool("jump", true);
    }
    private void PlayerAim(Vector2 delta)
    {
        if (aimON == false)
            return;

        _angleH += Mathf.Clamp(delta.x, -1.0f, 1.0f) * _aimSpeed * Time.deltaTime;
        _angleV += Mathf.Clamp(delta.y, -1.0f, 1.0f) * _aimSpeed * Time.deltaTime;

        _angleH = Mathf.Clamp(_angleH, -25.0f, 25.0f);
        _angleV = Mathf.Clamp(_angleV, -25.0f, 25.0f);

        Quaternion followTargetRotation = Quaternion.Euler((-1.0f) * _angleV, _angleH, 0);
        Quaternion characterRotation = Quaternion.Euler(0, _angleH, 0);

        _playerComponents.followTarget.rotation = followTargetRotation;
        _playerComponents.transform.rotation = characterRotation;

    }

    #region HAMMER
    GameObject _hammerInHand;
    float _f1 = 0f;
    float _hammerFocusingTime = 1.5f;
    float _hammerAreaLength = 9.1f;
    public void WhenHammerTaken()
    {
        _f1 = 0f;

        Vector3 pos = _playerComponents.hammerSlot.position;
        Quaternion rot = _playerComponents.hammerSlot.rotation;
        _hammerInHand = ObjectPool.Instance.InstantiatePooledObj(4, pos, rot, _playerComponents.hammerSlot);

        foreach (GameObject weapon in _playerComponents.weapons)
            weapon.SetActive(false);

        _playerComponents.animator.SetBool("hammer", true);
        GameManager.Instance.onGameRunning += ThrowHammer;
        _playerComponents.hammerLine.GetChild(0).gameObject.SetActive(true);
        _playerComponents.hammerLine.GetChild(0).localScale = Vector3.one;

        StartCoroutine(AddtionalRequirement.DoSlowmotion(0.2f, _hammerFocusingTime));
    }
    public void ThrowHammer()
    {
        _f1 += Time.unscaledDeltaTime;
        _f1 = Mathf.Clamp(_f1, 0f, _hammerFocusingTime);

        float hammerBlend = _f1 * (1f / _hammerFocusingTime);
        _playerComponents.animator.SetFloat("hammerBlend", hammerBlend);

        Vector3 newScale = _playerComponents.hammerLine.localScale;
        float newScaleZ = _f1 * (_hammerAreaLength / _hammerFocusingTime) + 1f;
        newScale.z = newScaleZ;
        _playerComponents.hammerLine.localScale = newScale;

        if (_f1 >= _hammerFocusingTime)
        {
            GameManager.Instance.onGameRunning -= ThrowHammer;

            _playerComponents.animator.SetBool("throwingHammer", true);
            _playerComponents.animator.SetBool("hammer", false);

            HammerAimLine hammerAimLine = _playerComponents.hammerLine.GetChild(0).gameObject.GetComponent<HammerAimLine>();
            hammerAimLine.KillEnemies(ref _killedEnemyNumber, ref _level);
            hammerAimLine.gameObject.SetActive(false);

            ObjectPool.Instance.DestroyPooledObj(_hammerInHand);

            Action activateWeapons = delegate
            {
                foreach (GameObject weapon in _playerComponents.weapons)
                    weapon.SetActive(true);
            };

            StartCoroutine(AddtionalRequirement.WaitTimeAndDo(activateWeapons, 0.75f));

            Vector3 thunderVFXPos = this.gameObject.transform.position + Vector3.up * 1.5f + Vector3.forward * 1.5f;
            GameObject thunderVFX = ObjectPool.Instance.InstantiatePooledObj(2, thunderVFXPos, Quaternion.identity);
            ObjectPool.Instance.DestroyPooledObj(thunderVFX, 10f);

            _f1 = 0f;
            _playerComponents.hammerLine.localScale = Vector3.one;

            _playerComponents.levelUI.SetLevel(_level);
            NinjaLevelUI.CheckAllEnemyUI();
        }
    }
    #endregion

    #region AXE
    float _f0 = 0f;
    float _spinTime = 0.5f;
    bool _axeThrowing = false;
    bool _readyToThrowAxe = false;
    GameObject _axeInHand;
    Vector3 _aimPoint;

    public void PickUpAxe()
    {
        collectedAxe++;
    }

    private void ThrowAxe()
    {
        if (collectedAxe <= 0)
        {
            UIGameSceneManager.Instance.gameplayPanel.SwitchCrosshair(false);
            GameManager.Instance.onGameRunning -= ThrowAxe;
            _playerComponents.animator.SetBool("BossFight", false);
            //Debug.Log("Throw axe discarded!");
            return;
        }

        if (_axeThrowing == true)
            return;

        AimFocusToThrowAxe();

        if (_readyToThrowAxe == true)
            StartCoroutine(IEThrowAxe());

    }
    private void AimFocusToThrowAxe()
    {
        if (aimON == false)
            return;

        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = CameraManager.Instance.mainCam.ScreenPointToRay(center);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, _layerMask))
        {
            _f0 += Time.deltaTime;
            _aimPoint = raycastHit.point;
        }

        else
            _f0 -= Time.deltaTime;

        _f0 = Mathf.Clamp(_f0, 0.0f, _aimFocusingTime);

        float axeBlend = _f0 * (1f / _aimFocusingTime);
        _playerComponents.animator.SetFloat("AxeBlend", axeBlend);

        if (_f0 >= _aimFocusingTime)
            _readyToThrowAxe = true;

    }
    private IEnumerator IEThrowAxe()
    {
        _axeThrowing = true;
        _readyToThrowAxe = false;
        _f0 = 0.0f;
        _playerComponents.animator.SetBool("ThrowingAxe", true);
        _playerComponents.animator.SetFloat("AxeBlend", 0.0f);

        _axeInHand.transform.parent = null;
        _axeInHand.transform.eulerAngles = new Vector3(0f, 90f, 0f);
        _axeInHand.transform.DORotate(new Vector3(0f, 0f, 360f), _spinTime, RotateMode.LocalAxisAdd).SetLoops(-1).SetEase(Ease.Linear);
        Vector3 direction = (_aimPoint - _axeInHand.transform.position).normalized;
        _axeInHand.GetComponent<Rigidbody>().AddForce(direction * _axeThrowPower, ForceMode.Impulse);
        _axeInHand = null;
        collectedAxe--;

        yield return new WaitForSeconds(0.65f);

        _axeThrowing = false;
        _playerComponents.animator.SetBool("ThrowingAxe", false);

        GetNewAxe();
    }
    private void GetNewAxe()
    {
        if (collectedAxe <= 0)
            return;

        Vector3 pos = _playerComponents.axeSlot.position;
        Quaternion rot = _playerComponents.axeSlot.rotation;

        _axeInHand = ObjectPool.Instance.InstantiatePooledObj(0, pos, rot, _playerComponents.axeSlot);
        _axeInHand.GetComponent<Axe>().Initialize(_axeDamage);
    }
    #endregion

    private IEnumerator IEFall()
    {
        canPlayerRun = false;
        _playerComponents.rb.isKinematic = true;
        _playerComponents.animator.SetTrigger("fall");
        CameraManager.Instance.PlayerFallCam();

        yield return new WaitForSeconds(1f);

        _playerComponents.rb.isKinematic = false;
        _playerComponents.coll.isTrigger = true;
        _playerComponents.rb.AddForce(Vector3.down * 10, ForceMode.Impulse);

        yield return new WaitForSeconds(3f);

        GameManager.Instance.InvokeGameEnd(false);

        yield return new WaitForSeconds(5f);
        _playerComponents.rb.isKinematic = true;
    }

    private int _n0 = 0;
    private void StandardHitEnemy()
    {
        if(_n0 == 0)
            _playerComponents.animator.SetBool("hit1", true);
        else
            _playerComponents.animator.SetBool("hit1trigger", true);

        _n0++;

        //SoundEffectManager.Instance.GetSound();

        //Debug.Log("Standard Hit Enemy");

    }
    private void EndStandardHits()
    {
        _n0 = 0;
        hitting = false;
        _playerComponents.animator.SetBool("hit1", false);
    }
    private IEnumerator IsStandardHitsEnded(float duration)
    {
        int n = -1;
        bool b = true;

        while (b)
        {
            if (n == _n0)
            {
                b = false;
                EndStandardHits();
            }

            else
            {
                n = _n0;
                yield return new WaitForSeconds(duration);
            }
        }

        yield return null;
    }
    private void SelectEndingDance()
    {
        int n = UnityEngine.Random.Range(0, 3);

        switch (n)
        {
            case 0:
                _playerComponents.animator.SetFloat("selectDance", 0f);
                break;
            case 1:
                _playerComponents.animator.SetFloat("selectDance", 0.5f);
                break;
            case 2:
                _playerComponents.animator.SetFloat("selectDance", 1f);
                break;
        }

    }
    private IEnumerator IEOnArrivedBoss()
    {
        canPlayerRun = false;
        canPlayerMoveLeftRight = false;

        GameManager.Instance.onGameRunning -= PlayerRunOnPlatform;

        _playerComponents.levelUI.gameObject.SetActive(false);

        foreach (GameObject weapon in _playerComponents.weapons)
            weapon.SetActive(false);

        Vector3 pos = _playerComponents.tr.position;
        pos.x = 0f;
        _playerComponents.tr.position = pos;

        _playerComponents.animator.SetBool("idle", true);

        yield return null;

        //yield return new WaitForSeconds(5.75f);

        //GameManager.Instance.onGameRunning += ThrowAxe;
        //_playerComponents.animator.SetBool("BossFight", true);
        //GetNewAxe();

        //yield return null;

        //aimON = true;
    }
    private IEnumerator IEOnStartedBossFight()
    {
        yield return new WaitForSeconds(CameraManager.AimCamBlendTime - 0.1f);

        GameManager.Instance.onGameRunning += ThrowAxe;
        _playerComponents.animator.SetBool("BossFight", true);
        GetNewAxe();

        yield return null;

        aimON = true;
    }
    
    private IEnumerator IEOnFightEnded()
    {
        yield return new WaitForSeconds(0.75f);

        SelectEndingDance();
        _playerComponents.animator.SetBool("dance", true);

    }
    private void OnGameStarted()
    {
        _playerComponents.rb.isKinematic = false;
        _playerComponents.rb.useGravity = false;
        _playerComponents.animator.SetBool("running", true);
        canPlayerRun = true;
        canPlayerMoveLeftRight = true;
    }
    private IEnumerator IEOnBossDead()
    {
        yield return new WaitForSeconds(2f);

        Quaternion rot = Quaternion.Euler(new Vector3(0f, 180f, 0f));
        _playerComponents.tr.rotation = rot;

        SelectEndingDance();
        _playerComponents.animator.SetBool("dance", true);

        //yield return new WaitForSeconds(5f);

        //GameManager.Instance.InvokeGameEnd(true);
    }
    private void AffectHealth(float amount)
    {
        _health += amount;

        //_status.health += amount;
        //_status.health = Mathf.Clamp(_status.health, 0f, NinjaWarriorStatus.maxHealth);   // TO DO: Max health may be changed.
    }
    private IEnumerator IEKilledByFakePlayer(Vector3 targetDirection, float force, float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);

        _playerComponents.coll.enabled = false;
        _playerComponents.rb.isKinematic = true;
        _playerComponents.animator.enabled = false;
        RagdollController ragdoll = GetComponent<RagdollController>();
        ragdoll.RagdollON();
        Rigidbody hipRb = ragdoll.ragdollBits[0].GetComponent<Rigidbody>();
        Vector3 dir = targetDirection.normalized;
        hipRb.AddForce(dir * force, ForceMode.Impulse);
        StartCoroutine(AddtionalRequirement.DoSlowmotion(0.05f, 2f));

        yield return new WaitForSeconds(3f);

        GameManager.Instance.InvokeFightEnded(false);
    }
    private IEnumerator IEKilledByEnemyStandard()
    {
        GameInputManager.Instance.DisablePlayerActions();

        canPlayerRun = false;

        _playerComponents.coll.enabled = false;
        _playerComponents.rb.isKinematic = true;
        _playerComponents.animator.enabled = false;
        _playerComponents.levelUI.gameObject.SetActive(false);

        GetComponent<RagdollController>().RagdollON();

        _playerComponents.hip.GetComponent<Rigidbody>().AddForce(Vector3.up * 2000, ForceMode.Impulse);

        yield return new WaitForSeconds(2f);

        GameManager.Instance.InvokeGameEnd(false);
    }

    public void InvokeOnPickUpAxe()
    {
        onPickUpAxe?.Invoke();
    }
    public void FallIntoGap()
    {
        StartCoroutine(IEFall());
    }
    public void KillEnemyStandard()
    {
        _level++;
        _killedEnemyNumber++;

        if (hitting == false)
        {
            hitting = true;
            StartCoroutine(IsStandardHitsEnded(0.25f));
        }

        StandardHitEnemy();
    }
    public void KilledByEnemyStandard()
    {
        StartCoroutine(IEKilledByEnemyStandard());
    }
    public void KilledByBoss()
    {
        _playerComponents.coll.enabled = false;
        _playerComponents.rb.isKinematic = true;
        _playerComponents.animator.enabled = false;
        RagdollController ragdoll = GetComponent<RagdollController>();
        ragdoll.RagdollON();
        Rigidbody hipRb = ragdoll.ragdollBits[0].GetComponent<Rigidbody>();
        Vector3 dir = new Vector3(1f, 1f, 0f).normalized;
        hipRb.AddForce(dir * 1000f, ForceMode.Impulse);
        StartCoroutine(AddtionalRequirement.DoSlowmotion(0.05f, 2f));
    }
    public void KilledByFakePlayer(Vector3 targetDirection, float force, float waitingTime)
    {
        StartCoroutine(IEKilledByFakePlayer(targetDirection, force, waitingTime));
    } 
    public void WhenBossCatches()
    {
        UIGameSceneManager.Instance.gameplayPanel.SwitchCrosshair(false);
        CameraManager.Instance.EndingCam(2f);
        GameManager.Instance.onGameRunning -= ThrowAxe;
        _playerComponents.animator.SetBool("BossFight", false);
        _playerComponents.axeSlot.gameObject.SetActive(false);
    }
    public void WhenDamageTaken(float enemyDamage)
    {
        float damageTaken = (-1f) * enemyDamage * (1 - _status.defenceRatio);
        AffectHealth(damageTaken);
    }
    public int RandomHitAnim()
    {
        int n = UnityEngine.Random.Range(0, 4);

        string animKey = $"atk{n + 1}";
        _playerComponents.animator.SetTrigger(animKey);

        return n;
    }

    //private IEnumerator IEOnArrivedFightArea()
    //{
    //    canPlayerRun = false;
    //    canPlayerMoveLeftRight = false;

    //    GameManager.Instance.onGameRunning -= PlayerRunOnPlatform;

    //    //var levelData = GameManager.Instance.currentLevelData;

    //    float t0 = 0f;
    //    float duration0 = 2.25f;

    //    Vector3 targetPos = GameManager.Instance.playerFightLocTr.position;
    //    Vector3 firstPos = _playerComponents.tr.position;
    //    _playerComponents.animator.SetTrigger("walk");

    //    while (t0 < duration0)    // süre düzeltilebilir
    //    {
    //        _playerComponents.tr.position = Vector3.Lerp(firstPos, targetPos, t0 / duration0);
    //        yield return null;
    //        t0 += Time.deltaTime;
    //    }

    //    _playerComponents.tr.position = targetPos;
    //    _playerComponents.animator.SetTrigger("idle2");

    //    yield return new WaitForSeconds(1f);
    //    isReadyToFight = true;
    //}
}

[Serializable]
public struct NinjaWarriorStatus
{
    public static readonly float minHealth = 300f;
    public static readonly float maxHealth = 5940f;
    public static readonly float minDamage = 50f;
    public static readonly float maxDamage = 1030f;
    public static readonly float minReducedDmg = 0f;
    public static readonly float maxReducedDmg = 980f;

    private static readonly float _k0 = 0.2f;

    [Header("Stats")]
    public int hp;
    public int attack;
    public int defence;

    [Header("Featured")]
    public float health;
    public float damage;
    public float defenceRatio;

    [Space(5)]
    public int power;
    public float exp;

    public static void Initialize(ref NinjaWarriorStatus ninjaStatus)
    {
        ninjaStatus.damage = minDamage + minDamage * (ninjaStatus.attack - 1) * _k0;
        ninjaStatus.defenceRatio = (ninjaStatus.defence - 1) / ((ninjaStatus.defence - 1) + 1f / _k0);
        ninjaStatus.health = minHealth + minHealth * (ninjaStatus.hp - 1) * _k0;

    }
}
