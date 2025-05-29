using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChangeButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private RectTransform _activePanel;
    [SerializeField] private RectTransform _deactivePanel;
    [SerializeField] private bool _active;

    [HideInInspector] public Button button { get { return _button; } }

    public void Swap(bool active)
    {
        _active = active;

        if (_active)
        {
            _button.interactable = true;
            _activePanel.gameObject.SetActive(true);
            _deactivePanel.gameObject.SetActive(false);
        }
        else
        {
            _button.interactable = false;
            _activePanel.gameObject.SetActive(false);
            _deactivePanel.gameObject.SetActive(true);
        }
    }
}
