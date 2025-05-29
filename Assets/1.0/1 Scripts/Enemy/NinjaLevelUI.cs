using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NinjaLevelUI : MonoBehaviour
{
    #region 01
    //private static List<EnemyLevelUI> EnemyLevelUIList = null;

    //public static void CheckAllEnemyUI()
    //{
    //    int playerLevel = GameManager.Instance.player.level;

    //    for (int i = 0; i < EnemyLevelUIList.Count; i++)
    //    {
    //        EnemyLevelUIList[i].Change(playerLevel);
    //    }

    //    Debug.Log("All Enemy Level UI changed!");
    //}

    //[SerializeField] private TextMeshProUGUI _levelTMP;
    //[SerializeField] private Image _bg;
    //[SerializeField] private int _level;

    //private void Awake()
    //{
    //    if (EnemyLevelUIList == null)
    //        EnemyLevelUIList = new List<EnemyLevelUI>();

    //    EnemyLevelUIList.Add(this);

    //    var canvas = transform.GetComponentInChildren<Canvas>();

    //    if (canvas.worldCamera)
    //        canvas.worldCamera = Camera.main;

    //    //Debug.Log("Enemy UI ADDED to the list");
    //}
    //private void OnDestroy()
    //{
    //    EnemyLevelUIList.Remove(this);
    //    //Debug.Log("Enemy UI DISCARDED from the list");
    //}

    //private void Change(int playerLevel)
    //{
    //    if (playerLevel < _level)
    //    {
    //        _bg.color = Color.red;
    //    }
    //    else
    //    {
    //        _bg.color = Color.green;
    //    }
    //}

    //public void SetLevel(int level)
    //{
    //    this._level = level;
    //    _levelTMP.text = $"{level}";
    //}
    #endregion

    #region 02
    private static List<NinjaLevelUI> EnemyLevelUIList = null;
    public static void CheckAllEnemyUI()
    {
        int playerLevel = GameManager.Instance.player.level;

        for (int i = 0; i < EnemyLevelUIList.Count; i++)
        {
            EnemyLevelUIList[i].Change(playerLevel);
        }

        //Debug.Log("All Enemy Level UI changed!");
    }

    [SerializeField] private bool _isPlayer;
    [SerializeField] private TextMeshProUGUI _levelTMP;
    [SerializeField] private Image _bg;
    [SerializeField] private int _level;

    private void Awake()
    {
        if (EnemyLevelUIList == null)
            EnemyLevelUIList = new List<NinjaLevelUI>();

        if(!_isPlayer)
            EnemyLevelUIList.Add(this);

        var canvas = transform.GetComponentInChildren<Canvas>();

        if (canvas.worldCamera)
            canvas.worldCamera = Camera.main;

        //Debug.Log("Enemy UI ADDED to the list");
    }

    private void Start()
    {
        var player = GameManager.Instance.player;

        if (_isPlayer)
        {
            player.onKilledEnemy += () =>
            {
                _level++;
                SetLevel(_level);
                CheckAllEnemyUI();
            };
        }
    }

    private void OnDestroy()
    {
        if (!_isPlayer)
        {
            EnemyLevelUIList.Remove(this);
            //Debug.Log("Enemy UI DISCARDED from the list");
        }
    }

    private void Change(int playerLevel)
    {
        if (playerLevel < _level)
        {
            _bg.color = Color.red;
        }
        else
        {
            _bg.color = Color.green;
        }
    }

    public void SetLevel()
    {
        _levelTMP.text = $"LEVEL {_level}";
    }
    public void SetLevel(int level)
    {
        _level = level;
        _levelTMP.text = $"LEVEL {_level}";
    }
    
    #endregion
}
