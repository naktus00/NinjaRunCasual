using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMarket : MonoBehaviour
{
    [SerializeField] private MarketDataNEW _marketData;
    [SerializeField] private PlayerMarket _player;
    [SerializeField] private Camera _marketCamera;

    [Space(10)]
    [SerializeField] private int _viewedWeaponIndex;
    [SerializeField] private int _viewedWeaponColorIndex;
    [SerializeField] private int _viewedSkinIndex;

    [Space(10)]
    [SerializeField] private int _weaponNumber;
    [SerializeField] private int _viewvedWeaponColorNumber;
    [SerializeField] private int _skinNumber;

    [Space(10)]
    [Header("UI ELEMENTS")]
    [SerializeField] private UIChangeButton _changeWeaponR;
    [SerializeField] private UIChangeButton _changeWeaponL;
    [SerializeField] private UIChangeButton _changeWeaponColorR;
    [SerializeField] private UIChangeButton _changeWeaponColorL;
    [SerializeField] private UIChangeButton _changeSkinR;
    [SerializeField] private UIChangeButton _changeSkinL;

    [Space(5)]
    [SerializeField] private Button _buyWeaponButton;
    [SerializeField] private TextMeshProUGUI _weaponCostTMP;

    [SerializeField] private Button _buyWeaponColorButton;
    [SerializeField] private TextMeshProUGUI _weaponColorCostTMP;

    [SerializeField] private Button _buySkinButton;
    [SerializeField] private TextMeshProUGUI _skinCostTMP;

    [Space(5)]
    [SerializeField] private TextMeshProUGUI _weaponNameTMP;
    [SerializeField] private TextMeshProUGUI _weaponColorNameTMP;
    [SerializeField] private TextMeshProUGUI _skinNameTMP;

    private void OnEnable()
    {
        _weaponNumber = _marketData.weapons.Length;
        _skinNumber = _marketData.skins.Length;

        var user = DataManager.Instance.user;
        _viewedWeaponIndex = user.selectedWeaponIndex;
        _viewedWeaponColorIndex = user.selectedWeaponColorIndex;
        _viewedSkinIndex = user.selectedSkinIndex;

        _viewvedWeaponColorNumber = _marketData.weapons[_viewedWeaponIndex].colorNumber;

        _buyWeaponButton.gameObject.SetActive(false);
        _buyWeaponColorButton.gameObject.SetActive(false);
        _buySkinButton.gameObject.SetActive(false);

        _changeWeaponR.Swap(true);
        _changeWeaponL.Swap(true);
        _changeWeaponColorR.Swap(true);
        _changeWeaponColorL.Swap(true);
        _changeSkinR.Swap(true);
        _changeSkinL.Swap(true);

        _weaponNameTMP.text = $"{_marketData.weapons[_viewedWeaponIndex].weaponName}";
        _weaponColorNameTMP.text = $"Color0{_viewedWeaponColorIndex}";
        _skinNameTMP.text = $"{_marketData.skins[_viewedSkinIndex].skinName}";

        _player.gameObject.SetActive(true);
        _marketCamera.gameObject.SetActive(true);

        _player.Initialize(_viewedWeaponIndex, _viewedWeaponColorIndex, _viewedSkinIndex);
    }
    private void Start()
    {
        _changeWeaponR.button.onClick.AddListener(() =>
        {
            ChangeWeaponButton(1);
        });
        _changeWeaponL.button.onClick.AddListener(() =>
        {
            ChangeWeaponButton(-1);
        });
        _changeWeaponColorR.button.onClick.AddListener(() =>
        {
            ChangeWeaponCollorButton(1);
        });
        _changeWeaponColorL.button.onClick.AddListener(() =>
        {
            ChangeWeaponCollorButton(-1);
        });
        _changeSkinR.button.onClick.AddListener(() =>
        {
            ChangeSkinButton(1);
        });
        _changeSkinL.button.onClick.AddListener(() =>
        {
            ChangeSkinButton(-1);
        });
        _buyWeaponButton.onClick.AddListener(() =>
        {
            BuyWeapon();
        });
        _buyWeaponColorButton.onClick.AddListener(() =>
        {
            BuyWeaponColor();
        });
        _buySkinButton.onClick.AddListener(() =>
        {
            BuySkin();
        });
    }
    private void OnDisable()
    {
        _player.gameObject.SetActive(false);
        _marketCamera.gameObject.SetActive(false);
    }
    
    private void ChangeWeaponButton(int amount)
    {
        _viewedWeaponIndex += amount;
        _viewedWeaponIndex = _viewedWeaponIndex % _weaponNumber;

        if (_viewedWeaponIndex < 0)
            _viewedWeaponIndex += _weaponNumber;

        _weaponNameTMP.text = $"{_marketData.weapons[_viewedWeaponIndex].weaponName}";

        _viewvedWeaponColorNumber = _marketData.weapons[_viewedWeaponIndex].colorNumber;
        _viewedWeaponColorIndex = 0;
        _buyWeaponColorButton.gameObject.SetActive(false);

        _weaponColorNameTMP.text = $"Color{_viewedWeaponColorIndex}";

        var user = DataManager.Instance.user;
        bool isPurchased = user.purchasedWeapons[_viewedWeaponIndex];

        if (isPurchased)
        {
            _buyWeaponButton.gameObject.SetActive(false);

            _changeWeaponColorR.Swap(true);
            _changeWeaponColorL.Swap(true);

            user.selectedWeaponIndex = _viewedWeaponIndex;
            user.selectedWeaponColorIndex = _viewedWeaponColorIndex;
        }
        else
        {
            _buyWeaponButton.gameObject.SetActive(true);

            int cost = _marketData.weapons[_viewedWeaponIndex].weaponCost;
            _weaponCostTMP.text = $"{cost}";

            _changeWeaponColorR.Swap(false);
            _changeWeaponColorL.Swap(false);
        }

        _player.ChangeWeapons(_viewedWeaponIndex);
        _player.ChangeWeaponsMaterial(_viewedWeaponColorIndex);

        _player.PlayAnimation(0);
    }
    private void ChangeWeaponCollorButton(int amount)
    {
        _viewedWeaponColorIndex += amount;
        _viewedWeaponColorIndex = _viewedWeaponColorIndex % _viewvedWeaponColorNumber;

        if (_viewedWeaponColorIndex < 0)
            _viewedWeaponColorIndex += _viewvedWeaponColorNumber;

        _weaponColorNameTMP.text = $"Color{_viewedWeaponColorIndex}";

        var user = DataManager.Instance.user;
        bool isPurchased = user.purchasedWeaponColors[_viewedWeaponIndex][_viewedWeaponColorIndex];

        if (isPurchased)
        {
            _buyWeaponColorButton.gameObject.SetActive(false);

            user.selectedWeaponColorIndex = _viewedWeaponColorIndex;
        }
        else
        {
            _buyWeaponColorButton.gameObject.SetActive(true);

            int cost = _marketData.weapons[_viewedWeaponIndex].colorsCosts[_viewedWeaponColorIndex];
            _weaponColorCostTMP.text = $"{cost}";
        }

        _player.ChangeWeaponsMaterial(_viewedWeaponColorIndex);
        _player.PlayAnimation(0);
    }
    private void ChangeSkinButton(int amount)
    {
        _viewedSkinIndex += amount;
        _viewedSkinIndex = _viewedSkinIndex % _skinNumber;

        if (_viewedSkinIndex < 0)
            _viewedSkinIndex += _skinNumber;

        _skinNameTMP.text = $"{_marketData.skins[_viewedSkinIndex].skinName}";

        var user = DataManager.Instance.user;
        bool isPurchased = user.purchasedSkins[_viewedSkinIndex];

        if (isPurchased)
        {
            _buySkinButton.gameObject.SetActive(false);

            user.selectedSkinIndex = _viewedSkinIndex;
        }
        else
        {
            _buySkinButton.gameObject.SetActive(true);

            int cost = _marketData.skins[_viewedSkinIndex].cost;
            _skinCostTMP.text = $"{cost}";
        }

        _player.ChangeSkin(_viewedSkinIndex);
        _player.PlayAnimation(0);
    }

    private void BuyWeapon()
    {
        var user = DataManager.Instance.user;

        int cost = _marketData.weapons[_viewedWeaponIndex].weaponCost;

        if (TradeAdditional.Buy(cost))
        {
            user.purchasedWeapons[_viewedWeaponIndex] = true;
            user.purchasedWeaponColors[_viewedWeaponIndex][0] = true;

            user.selectedWeaponIndex = _viewedWeaponIndex;
            user.selectedWeaponColorIndex = _viewedWeaponColorIndex;

            _changeWeaponColorR.Swap(true);
            _changeWeaponColorL.Swap(true);

            _buyWeaponButton.gameObject.SetActive(false);

            _player.PlayAnimation(1);
        }

        
    }
    private void BuyWeaponColor()
    {
        var user = DataManager.Instance.user;

        int cost = _marketData.weapons[_viewedWeaponIndex].colorsCosts[_viewedWeaponColorIndex];

        if (TradeAdditional.Buy(cost))
        {
            user.purchasedWeaponColors[_viewedWeaponIndex][_viewedWeaponColorIndex] = true;
            user.selectedWeaponColorIndex = _viewedWeaponColorIndex;

            _buyWeaponColorButton.gameObject.SetActive(false);

            _player.PlayAnimation(2);
        }
    }
    private void BuySkin()
    {
        var user = DataManager.Instance.user;

        int cost = _marketData.skins[_viewedSkinIndex].cost;

        if (TradeAdditional.Buy(cost))
        {
            user.purchasedSkins[_viewedSkinIndex] = true;
            user.selectedSkinIndex = _viewedSkinIndex;

            _buySkinButton.gameObject.SetActive(false);

            _player.PlayAnimation(3);
        }
    }

}
