using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseController : MonoBehaviour
{
    public static MouseController Instance;

    public Texture2D finger, attack, transport;
    RaycastHit hitInfo;
    // Singleton Pattern
    public event Action<Vector3> OnMouseClicked; // event is the delegate generic class of Action
                                                 // so it doesn't need to be defined again in other places.

    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;
    }

    void Update() {
        SetCursorTexture();
        MouseControl();
    }

    void SetCursorTexture()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hitInfo))
        {
            // Changing mouse texture
        }
    }

    void MouseControl() {
        if(Input.GetMouseButton(0) && hitInfo.collider != null)
        {
            if(hitInfo.collider.gameObject.CompareTag("Ground"))
            {
                OnMouseClicked?.Invoke(hitInfo.point);
            }
        }
    }
}
