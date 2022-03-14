/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }
public class KeyboardManager : MonoBehaviour
{
    RaycastHit hitInfo;
    public EventVector3 OnKeyboardPressed;
    public float speed = 6f;
    void Update()
    {
        SetCursorTexture();
        KeyboardControl();
    }

    void SetCursorTexture()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
        {
            // Changing mouse texture
        }
    }

    void KeyboardControl()
    {
        float ad_input = Input.GetAxisRaw("Horizontal"); 
        float ws_input = Input.GetAxisRaw("Vertical");

        if (ad_input != 0 && ws_input != 0 && hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.CompareTag("Ground"))
            {
                hitInfo = 
                OnKeyboardPressed?.Invoke(hitInfo.point);
            }
        }
    }
}
*/