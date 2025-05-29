using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISuccessPanel : MonoBehaviour
{
    [SerializeField] private RectTransform[] _starRects;
    [SerializeField] private RectTransform _infoDefeatBoss;
    [SerializeField] private TextMeshProUGUI _infoKilledAllEnemiesTMP;
    [SerializeField] private TextMeshProUGUI _rewardCoinTMP;
    [SerializeField] private TextMeshProUGUI _rewardLevelTMP;
    [SerializeField] private Button _btnNextLevel;
    [SerializeField] private Button _btnReturnMenu;

    private int _openStarNumber;
    private bool _killedBoss;
    private int _killedEnemies;
    private int _rewardCoin;
    private int _rewardKilledBossCoin;
    private int _rewardLevel;

    private void Start()
    {
        _btnNextLevel.onClick.AddListener(() => { GameSceneManager.Instance.LoadSceneAsync(GAMESCENES.GAME); });
        _btnReturnMenu.onClick.AddListener(() => { GameSceneManager.Instance.LoadSceneAsync(GAMESCENES.MAIN); });
    }

    public void Open()
    {
        this.gameObject.SetActive(true);

        _killedBoss = GameManager.Instance.killedBoss;
        _killedEnemies = GameManager.Instance.player.killedEnemyNumber;
        _rewardCoin = GameManager.Instance.rewardCoin;
        _rewardKilledBossCoin = GameManager.Instance.rewardKilledBossCoin;
        _rewardLevel = GameManager.Instance.rewardPlayerLevel;

        _openStarNumber = 0;

        int coin = _rewardCoin;

        if (_killedBoss == true)
        {
            _openStarNumber += 1;
            coin += _rewardKilledBossCoin; 
        }

        if (_killedEnemies > GameManager.Instance.totalEnemyNumber / 2f)
            _openStarNumber += 2;
        else
            _openStarNumber += 1;

        for (int i = 0; i < _starRects.Length; i++)
        {
            if(i < _openStarNumber)
            {
                _starRects[i].GetChild(0).gameObject.SetActive(true);
                _starRects[i].GetChild(1).gameObject.SetActive(false);
            }
        }

        if (_killedBoss)
        {
            _infoDefeatBoss.GetChild(0).gameObject.SetActive(true);
            _infoDefeatBoss.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            _infoDefeatBoss.GetChild(0).gameObject.SetActive(false);
            _infoDefeatBoss.GetChild(1).gameObject.SetActive(true);
        }

        _infoKilledAllEnemiesTMP.text = $"{_killedEnemies}";
        _rewardCoinTMP.text = $"{coin}";
        _rewardLevelTMP.text = $"+{_rewardLevel}";
    }
}
