using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIGameSceneManager : MonoBehaviour
{
    private static UIGameSceneManager _instance;
    public static UIGameSceneManager Instance { get { return _instance; } }

    [SerializeField] private UIMenu[] _menus;

    [SerializeField] private TextMeshProUGUI _levelTMP;

    [Header("Starting Panel")]
    [SerializeField] private Button _btnStartGame;

    [Header("Pause Panel")]
    [SerializeField] private Button _btnContinue;
    [SerializeField] private Button _btnReturnMenu01;

    [Header("Game Play Panel")]
    [SerializeField] public UIGameplayPanel gameplayPanel;  // Private yapýlacak.

    [Header("Level End Panel")]
    [SerializeField] private UILevelEndPanel _levelEndPanel;

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

        CloseMenu();
    }
    private void Start()
    {
        SubscribeGameActions();

        _btnStartGame.onClick.AddListener(() =>
        {
            GameManager.Instance.InvokeGameStart();
        });

        _btnContinue.onClick.AddListener(() =>
        {
            GameManager.Instance.InvokeGamePause();
        });

        _btnReturnMenu01.onClick.AddListener(() =>
        {
            GameSceneManager.Instance.LoadSceneAsync(GAMESCENES.MAIN);
            _btnContinue.interactable = false;
            _btnReturnMenu01.interactable = false;
        });

        gameplayPanel.Initialize();
        _levelEndPanel.Initialize();

        int level = GameManager.Instance.level;
        _levelTMP.text = $"LEVEL {level}";

        OpenMenu("Starting Panel");
    }
    private void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }

    private void SubscribeGameActions()
    {
        GameManager.Instance.onGameStarted += () => 
        { 
            OpenMenu("Gameplay Menu");
        };

        GameManager.Instance.onGamePaused += (paused) =>
        {
            if(paused)
                OpenMenu("Pause Menu");
            else
                OpenMenu("Gameplay Menu");
        };

        GameManager.Instance.onGameEnd += (win) =>
        {
            OpenMenu("Level End Menu");
        };

    }

    public void OpenMenu(string targetMenuName)
    {
        foreach (UIMenu menu in _menus)
        {
            if (menu.menuName == targetMenuName)
            {
                menu.Open();
            }

            else
            {
                if (menu.open == true)
                    menu.Close();
            }
        }
    }
    public void OpenMenu(UIMenu targetMenu)
    {
        foreach (UIMenu menu in _menus)
        {
            if (menu == targetMenu)
            {
                menu.Open();
            }
            else
            {
                if (menu.open == true)
                    menu.Close();
            }
        }
    }
    public void CloseMenu(string targetMenuName)
    {
        foreach (UIMenu menu in _menus)
        {
            if (menu.menuName == targetMenuName)
            {
                if (menu.gameObject.activeInHierarchy == true)
                    menu.Close();

                break;
            }
        }
    }
    public void CloseMenu(UIMenu targetMenu)
    {
        if (targetMenu.gameObject.activeInHierarchy == true)
            targetMenu.Close();
    }
    public void CloseMenu()
    {
        foreach (UIMenu menu in _menus)
        {
            if (menu.gameObject.activeInHierarchy == true)
                menu.Close();
        }
    }


}
