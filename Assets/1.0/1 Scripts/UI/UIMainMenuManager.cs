using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMainMenuManager : UIManager
{
    private static UIMainMenuManager _instance;
    public static UIMainMenuManager Instance { get { return _instance; } }

    [SerializeField] private TextMeshProUGUI _coinInfoTMP;

    [Space(10)]
    [SerializeField] private UIMainMenuButton _settingsButton;
    [SerializeField] private UIMainMenuButton _homeButton;
    [SerializeField] private UIMainMenuButton _marketButton;

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

        _settingsButton.Swap(false);
        _homeButton.Swap(false);
        _marketButton.Swap(false);
    }
    private void Start()
    {
        _settingsButton.button.onClick.AddListener(() =>
        {
            if (IsMenuOpen("Menu Settings") == true)
                return;

            _settingsButton.Swap(true);
            _homeButton.Swap(false);
            _marketButton.Swap(false);

            OpenMenu("Menu Settings");
            // Add audio effect
        });
        _homeButton.button.onClick.AddListener(() =>
        {
            if (IsMenuOpen("Menu Home") == true)
                return;

            _settingsButton.Swap(false);
            _homeButton.Swap(true);
            _marketButton.Swap(false);

            OpenMenu("Menu Home");
            // Add audio effect
        });
        _marketButton.button.onClick.AddListener(() =>
        {
            if (IsMenuOpen("Menu Market") == true)
                return;

            _settingsButton.Swap(false);
            _homeButton.Swap(false);
            _marketButton.Swap(true);

            OpenMenu("Menu Market");
            // Add audio effect
        });

        _settingsButton.Swap(false);
        _homeButton.Swap(true);
        _marketButton.Swap(false);

        TradeAdditional.OnBought += () =>
        {
            int coin = DataManager.Instance.user.coin;
            _coinInfoTMP.text = $"{coin}";
        };

        int coin = DataManager.Instance.user.coin;
        _coinInfoTMP.text = $"{coin}";

        OpenMenu("Menu Home");
        
    }
    private void OnDestroy()
    {
        if (_instance == this)
            _instance = null;

        TradeAdditional.OnBought -= () =>
        {
            int coin = DataManager.Instance.user.coin;
            _coinInfoTMP.text = $"{coin}";
        };
    }

    public void StartGame()
    {
        GameSceneManager.Instance.LoadSceneAsync(GAMESCENES.GAME);
    }
}
