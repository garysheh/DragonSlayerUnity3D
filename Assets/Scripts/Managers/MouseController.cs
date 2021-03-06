using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseController : Singleton<MouseController>
{ 
    public Texture2D finger, attack, transport, talk;
    RaycastHit hitInfo;
    // Singleton Pattern
    public event Action<Vector3> OnMouseClicked; // event is the delegate generic class of Action
                                                 // so it doesn't need to be defined again in other places.

    public event Action<GameObject> OnEnemyClicked;


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
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
            switch (hitInfo.collider.gameObject.tag)
            {
                case "Ground":
                    Cursor.SetCursor(finger, new Vector2(16, 16), CursorMode.Auto);
                    break;
                case "Enemy":
                    Cursor.SetCursor(attack, new Vector2(16, 16), CursorMode.Auto);
                    break;
                case "Portal":
                    Cursor.SetCursor(transport, new Vector2(16, 16), CursorMode.Auto);
                    break;
            }
        }
    }

    void MouseControl() {

        if(Input.GetMouseButtonDown(1) && hitInfo.collider != null)
        {
            if(hitInfo.collider.gameObject.CompareTag("Ground"))
            {
                OnMouseClicked?.Invoke(hitInfo.point);
            }
            if (hitInfo.collider.gameObject.CompareTag("Enemy"))
            {
                OnEnemyClicked?.Invoke(hitInfo.collider.gameObject);
            }
            if (hitInfo.collider.gameObject.CompareTag("Portal"))
            {
                OnMouseClicked?.Invoke(hitInfo.point);
            }
        }
    }
}
