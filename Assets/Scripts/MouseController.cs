using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseController : MonoBehaviour
{
    public static MouseController Instance;

    public Texture2D finger, attack, transport, talk;
    RaycastHit hitInfo;
    // Singleton Pattern
    public event Action<Vector3> OnMouseClicked; // event is the delegate generic class of Action
                                                 // so it doesn't need to be defined again in other places.

    public event Action<GameObject> OnEnemyClciked;


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
            switch (hitInfo.collider.gameObject.tag)
            {
                case "Ground":
                    Cursor.SetCursor(finger, new Vector2(16, 16), CursorMode.Auto);
                    break;
                case "Enemy":
                    Cursor.SetCursor(attack, new Vector2(16, 16), CursorMode.Auto);
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
                OnEnemyClciked?.Invoke(hitInfo.collider.gameObject);
            }
        }
    }
}
