using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHome : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nextLevelTMP;
    [SerializeField] private Button _startGameButton;

    private void OnEnable()
    {
        var user = DataManager.Instance.user;
        _nextLevelTMP.text = $"{user.nextLevel}";

        _startGameButton.gameObject.SetActive(true);
    }

    private void Start()
    {
        _startGameButton.onClick.AddListener(() =>
        {
            _startGameButton.gameObject.SetActive(false);
            UIMainMenuManager.Instance.StartGame();
        });
    }
}
