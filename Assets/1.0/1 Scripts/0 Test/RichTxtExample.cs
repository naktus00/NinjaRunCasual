using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RichTxtExample : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI TMP;
    [SerializeField] private int a;
    [SerializeField] private int b;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TMP.text = $"<color=red>{a}</color> + <color=blue>{b}</color>";
        }
    }
}
