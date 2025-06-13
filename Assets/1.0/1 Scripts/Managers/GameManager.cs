using DoroCode;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const float GameEndDelayTime = 3f;

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public event Action onGameStarted;
    public event Action onGameRunning;
    public event Action<bool> onGamePaused;
    public event Action<bool> onGameEnd;

    public event Action onArrivedBoss;
    public event Action onBossFightStarted;
    public event Action<bool> onBossFightOver;

    //---------
    public Action onArrivedFightArea;
    public Action onFightStarted;
    public Action<bool> onFightEnded;
    //public Action onPlayerWin;
    //public Action onPlayerLose;
    //---------


    [SerializeField] private GameData _gameData;
    [HideInInspector] public GameData gameData { get { return _gameData; } }

    [Header("Game")]
    [SerializeField] private bool _isGameRunning;
    [SerializeField] private bool _paused;

    [Header("Level")]
    [SerializeField] private int _level;
    [SerializeField] private bool _killedBoss;
    [SerializeField] private int _totalEnemyNumber;
    [SerializeField] private int _totalAxeNumber;
    [SerializeField] private int _rewardCoin;
    [SerializeField] private int _rewardKilledBossCoin;
    [SerializeField] private int _rewardPlayerLevel;

    private PlayerController _player;
    private EnemyBoss _boss;

    [HideInInspector] public int level { get { return _level; } }
    [HideInInspector] public bool killedBoss { get { return _killedBoss; } }
    [HideInInspector] public int totalEnemyNumber { get { return _totalEnemyNumber; } }
    [HideInInspector] public int totalAxeNumber { get { return _totalAxeNumber; } }
    [HideInInspector] public int rewardCoin { get { return _rewardCoin; } }
    [HideInInspector] public int rewardKilledBossCoin { get { return _rewardKilledBossCoin; } }
    [HideInInspector] public int rewardPlayerLevel { get { return _rewardPlayerLevel; } }
    [HideInInspector] public PlayerController player { get { return _player; } }
    [HideInInspector] public EnemyBoss boss { get { return _boss; } }

    private LevelManager _levelManager;

    //-----
    //[HideInInspector] public EnemyFakePlayer enemyPlayer;
    //[SerializeField] public Transform playerFightLocTr;
    //-----

    private void Awake()
    {
        #region Singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        #endregion

        ResetActions();
    }

    private void Start()
    {
        SubscribeGameActions();

        _level = DataManager.Instance.user.nextLevel; // Burasý User verilerinden çekilecek. !!!

        _levelManager = FindObjectOfType<LevelManager>();
        _levelManager.LoadLevel(_level);

        _totalEnemyNumber = _levelManager.currentLevelData.totalEnemyNumber;
        _totalAxeNumber = _levelManager.currentLevelData.totalCollactableAxeNumber;
        _rewardCoin = _levelManager.currentLevelData.rewardCoin;
        _rewardKilledBossCoin = _levelManager.currentLevelData.rewardKilledBossCoin;
        _rewardPlayerLevel = _levelManager.currentLevelData.rewardPlayerLevel;

        _player = FindObjectOfType<PlayerController>();
        _boss = FindObjectOfType<EnemyBoss>();

        _paused = false;
        _isGameRunning = false;
    }

    private void Update()
    {
        if (_isGameRunning)
            InvokeGameRunning();
    }
    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        } 
    }

    #region ACTIONS METHODS
    private void ResetActions()
    {
        onGameStarted = null;
        onGameRunning = null;
        onGamePaused = null;
        onGameEnd = null;

        onArrivedBoss = null;
        onBossFightStarted = null;
        onBossFightOver = null;
    }

    private void OnGameStarted()
    {
        _isGameRunning = true;
    }
    private void OnGameEnded(bool gameWin)
    {
        var user = DataManager.Instance.user;

        if (gameWin)
        {
            int nextlevel = _level + 1;
            user.nextLevel = nextlevel;

            user.coin += _killedBoss == true ? _rewardCoin + _rewardKilledBossCoin : _rewardCoin;
            user.playerLevel += _rewardPlayerLevel;
        }
    }
    private void OnGamePaused(bool paused)
    {
        _isGameRunning = !paused;
    }
    //----------
    private void OnArrivedFightArea()
    {
        StartCoroutine(IEOnArrivedFightArea());
    }
    private IEnumerator IEOnArrivedFightArea()
    {
        yield return new WaitUntil(() => _player.isReadyToFight == true);
        UIGameSceneManager.Instance.OpenMenu("Start Fight Panel");
    }

    private void InvokeGameRunning()
    {
        if (onGameRunning != null)
            onGameRunning();
    }
    //----------
    public void InvokeGameStart()
    {
        if (onGameStarted != null)
            onGameStarted();
    }
    public void InvokeGameEnd(bool gameWin)
    {
        if (onGameEnd != null)
            onGameEnd(gameWin);

        Debug.Log($"Game Ended ! WIN -> {gameWin}");
    }
    public void InvokeArrivedBoss()
    {
        //if (onArrivedBoss != null)
        //    onArrivedBoss();

        //Debug.Log("Invoke Arrived Boss");

        StartCoroutine(IEInvokeArrivedBoss());
    }
    private IEnumerator IEInvokeArrivedBoss()
    {
        if (onArrivedBoss != null)
            onArrivedBoss();

        Debug.Log("Invoke Arrived Boss");

        yield return new WaitForSeconds(EnemyBoss.AnimationRoaringTime);

        if (player.collectedAxe > 0)
        {
            if (onBossFightStarted != null)
                onBossFightStarted();
        }
        else
        {
            boss.KillPlayer();
        }
    }
    public void InvokeBossFightOver(bool killedBoss)
    {
        //if (onBossFightOver != null)
        //    onBossFightOver(killedBoss);

        //Debug.Log($"Invoke Boss Fight Over -> {killedBoss}");

        _killedBoss = killedBoss;

        if (onBossFightOver != null)
            onBossFightOver(killedBoss);

        Action action = () => { InvokeGameEnd(true); };
        StartCoroutine(AddtionalRequirement.WaitTimeAndDo(action, GameEndDelayTime));
    }
    public void InvokeBossFightOver(bool killedBoss, float gameEndDelay)
    {
        _killedBoss = killedBoss;

        if (onBossFightOver != null)
            onBossFightOver(killedBoss);

        Action action = () => { InvokeGameEnd(true); };
        StartCoroutine(AddtionalRequirement.WaitTimeAndDo(action, gameEndDelay));
    }
    public void InvokeGamePause()
    {
        _paused = !_paused;

        if (onGamePaused != null)
            onGamePaused(_paused);

        Debug.Log($"Invoke Game Pause -> {_paused}");
    }
    
    public void InvokeArrivedFightArea()
    {
        if (onArrivedFightArea != null)
            onArrivedFightArea();
    }
    //----------
    public void InvokeFightStarted()
    {
        if (onFightStarted != null)
            onFightStarted();

        Debug.Log("Fight Started!");
    }
    public void InvokeFightEnded(bool playerWin)
    {
        if (onFightEnded != null)
            onFightEnded.Invoke(playerWin);
    }
    //----------
    public void SubscribeGameActions()
    {
        onGameStarted += OnGameStarted;
        onGamePaused += onGamePaused;
        onGameEnd += OnGameEnded;

        //-----
        onArrivedFightArea += OnArrivedFightArea;
        //-----
    }
    #endregion

    #region GAME DATA METHODS
    // Save Load etc.
    #endregion
}
