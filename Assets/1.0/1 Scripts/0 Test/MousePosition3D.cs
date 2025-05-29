using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition3D : MonoBehaviour
{
    [SerializeField] private Camera camMain;
    [SerializeField] private LayerMask layerMask; 

    private void Update()
    {
        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = camMain.ScreenPointToRay(center);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, layerMask))
        {
            Debug.Log(raycastHit.transform.gameObject.name);
            transform.position = raycastHit.point;
        }
    }
}
