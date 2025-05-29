using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILevelEndPanel : MonoBehaviour
{
    [Header("Failed")]
    [SerializeField] private RectTransform _failedRect;
    [SerializeField] private Button _btnRetry;
    [SerializeField] private Button _btnReturnMenu01;

    [Space(10)]
    [Header("Success")]
    [SerializeField] private UISuccessPanel _successPanel;

    public void Initialize()
    {
        _btnRetry.onClick.AddListener(() => 
        {
            SceneManager.LoadScene(2);
            //GameSceneManager.Instance.LoadSceneAsync(GAMESCENES.GAME);
            _btnRetry.interactable = false;
            _btnReturnMenu01.interactable = false;
        });
        _btnReturnMenu01.onClick.AddListener(() => 
        {
            GameSceneManager.Instance.LoadSceneAsync(GAMESCENES.MAIN);
            _btnRetry.interactable = false;
            _btnReturnMenu01.interactable = false;
        });

        SubscribeActions();
    }

    private void SubscribeActions()
    {
        GameManager.Instance.onGameEnd += (win) =>
        {
            if (win)
                OnGameWin();
            else
                OnGameLose();
        };
    }
    private void OnGameWin()
    {
        _failedRect.gameObject.SetActive(false);
        _successPanel.Open();
    }
    private void OnGameLose()
    {
        _failedRect.gameObject.SetActive(true);
        _successPanel.gameObject.SetActive(false);
    }

}
