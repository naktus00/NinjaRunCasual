using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{
    private static GameInputManager _instance;
    public static GameInputManager Instance { get { return _instance; } }

    private GameInputActions _gameInputActions;
    public GameInputActions gameInputActions { get { return _gameInputActions; } }

    [SerializeField] private Vector2 _touchDelta;
    public Vector2 touchDelta { get { return _touchDelta; } }

    #region PLAYER ACTIONS
    private InputActionMap _currentInputActionMap;
    [HideInInspector] public InputActionMap playerStandardActions { get { return _gameInputActions.Player.Get(); } }
    [HideInInspector] public InputActionMap playerAxeActions { get { return _gameInputActions.PlayerAxe.Get(); } }
    #endregion

    #region ACTIONS
    /// <summary>
    /// Player left right movement on the platform
    /// </summary>
    public event Action<Vector2> playerMovementXAxis;

    /// <summary>
    /// Player aim when using axe
    /// </summary>
    public event Action<Vector2> axeAim;
    #endregion

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

        _touchDelta = Vector2.zero;
        _gameInputActions = new GameInputActions();
        _currentInputActionMap = playerStandardActions;

    }

    private void Start()
    {
        AssignActions();
        SubscribeActions();
    }

    private void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }

    private void ResetActions()
    {
        playerMovementXAxis = null;
        axeAim = null;
    }

    /// <summary>
    /// This method subscribes to input actions.
    /// </summary>
    private void AssignActions()
    {
        _gameInputActions.Player.PlayerTouchDelta.performed += ctx => { _touchDelta = ctx.ReadValue<Vector2>(); };
        _gameInputActions.Player.PlayerTouchDelta.performed += ctx => { playerMovementXAxis(touchDelta); };
        _gameInputActions.Player.PlayerTouchDelta.canceled += ctx => { _touchDelta = Vector2.zero; };

        _gameInputActions.PlayerAxe.TouchDelta.performed += ctx => { _touchDelta = ctx.ReadValue<Vector2>(); };
        _gameInputActions.PlayerAxe.TouchDelta.performed += ctx => { axeAim(touchDelta); };
        _gameInputActions.PlayerAxe.TouchDelta.canceled += ctx => { _touchDelta = Vector2.zero; };

    }

    private void SubscribeActions()
    {
        GameManager.Instance.onGameStarted += () => { ActivatePlayerActions(playerStandardActions); };
        GameManager.Instance.onGamePaused += (paused) =>
        {
            if (paused)
                DisablePlayerActions();
            else
                ActivatePlayerActions(_currentInputActionMap);
        };
        GameManager.Instance.onArrivedBoss += () => { ActivatePlayerActions(playerAxeActions); };
        GameManager.Instance.onBossFightOver += (playerWin) => { DisablePlayerActions(); };

        GameManager.Instance.onArrivedFightArea += () => { DisablePlayerActions(); };
    }

    public void ActivatePlayerActions(InputActionMap targetInputActionMap)
    {
        _currentInputActionMap.Disable();
        targetInputActionMap.Enable();
        _currentInputActionMap = targetInputActionMap;
    }

    public void DisablePlayerActions()
    {
        _currentInputActionMap.Disable();
    }
}
