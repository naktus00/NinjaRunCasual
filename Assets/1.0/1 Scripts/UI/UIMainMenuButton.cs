using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenuButton : MonoBehaviour
{
    [SerializeField] private bool _open;
    [SerializeField] private RectTransform[] _rects;
    [SerializeField] private Button _button;

    [HideInInspector] public Button button { get { return _button; } }
    public void Swap(bool open)
    {
        _open = open;

        if (_open)
        {
            _rects[0].gameObject.SetActive(true);
            _rects[1].gameObject.SetActive(false);
        }
        else
        {
            _rects[0].gameObject.SetActive(false);
            _rects[1].gameObject.SetActive(true);
        }
    }

}
