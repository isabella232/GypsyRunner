using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static event Action LeftMouseButtonClickedEvent;
    public static event Action LeftMouseButtonReleasedEvent;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            LeftMouseButtonClickedEvent?.Invoke();
        else if (Input.GetKeyUp(KeyCode.Mouse0))
            LeftMouseButtonReleasedEvent?.Invoke();
    }
}
