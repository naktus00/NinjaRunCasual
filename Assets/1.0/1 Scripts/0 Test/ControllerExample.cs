using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerExample : MonoBehaviour
{
    private float _speed = 5.0f;

    private void Awake()
    {
        
    }

    private void Start()
    {
        print("Controller example subscribed event!");
        SubscribeInputActions();
    }

    private void SubscribeInputActions()
    {
        GameInputManager.Instance.playerMovementXAxis += PlayerMovementXAxis;
    }

    private void PlayerMovementXAxis(Vector2 delta)
    {
        print("Player moving");
        print(delta.ToString());
        var translation = Vector3.zero;
        translation.x = delta.x * Time.deltaTime * _speed;
        transform.Translate(translation);
    }
}
