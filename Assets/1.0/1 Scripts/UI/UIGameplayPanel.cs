using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplayPanel : MonoBehaviour
{
    [SerializeField] private RectTransform crosshair;
    [SerializeField] private Button _btnPause;

    [Header("Main")]
    [SerializeField] private RectTransform _mainRect;
    [SerializeField] private TextMeshProUGUI _collectedAxesTMP;

    [Header("Boss")]
    [SerializeField] private RectTransform _bossRect;
    [SerializeField] private Slider _healthBarBoss;

    private float _bossHealth;
    private int _totalAxeNumber;
    private int _collectedAxeNumber;

    public void Initialize()
    {
        crosshair.gameObject.SetActive(false);

        _totalAxeNumber = GameManager.Instance.totalAxeNumber;
        _collectedAxeNumber = 0;

        _collectedAxesTMP.text = $"{_collectedAxeNumber}/{_totalAxeNumber}";

        _bossHealth = GameManager.Instance.boss.health;
        _healthBarBoss.minValue = 0f;
        _healthBarBoss.maxValue = _bossHealth;
        _healthBarBoss.value = _bossHealth;

        _btnPause.onClick.AddListener(() =>
        {
            GameManager.Instance.InvokeGamePause();
        });

        SubscribeGameActions();
    }

    private void SubscribeGameActions()
    {
        GameManager.Instance.onGameStarted += () =>
        {
            _mainRect.gameObject.SetActive(true);
            _bossRect.gameObject.SetActive(false);
        };
        GameManager.Instance.onArrivedBoss += () =>
        {
            _btnPause.gameObject.SetActive(false);
            _mainRect.gameObject.SetActive(false);
            _bossRect.gameObject.SetActive(true);
        };
        GameManager.Instance.onGamePaused += (paused) =>
        {
            _btnPause.gameObject.SetActive(!paused);
        };
        GameManager.Instance.onBossFightStarted += () => { StartCoroutine(IEOnBossFightStarted()); };

        var player = GameManager.Instance.player;

        player.onPickUpAxe += () =>
        {
            _collectedAxeNumber++;
            _collectedAxesTMP.text = $"{_collectedAxeNumber}/{_totalAxeNumber}";
        };

        var boss = GameManager.Instance.boss;

        boss.onHitted += () =>
        {
            _healthBarBoss.value = boss.health;
        };
    }

    private IEnumerator IEOnBossFightStarted()
    {
        yield return new WaitForSeconds(CameraManager.AimCamBlendTime - 0.1f);
        crosshair.gameObject.SetActive(true);
    }
    public void SwitchCrosshair(bool key)
    {
        crosshair.gameObject.SetActive(key);
    }
}
