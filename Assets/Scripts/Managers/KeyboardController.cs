using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KeyboardController : MonoBehaviour
{
    public static KeyboardController Instance;

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

    void Update()
    {
        SetCursorTexture();
        MouseControl();
    }

    void SetCursorTexture()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
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

    void MouseControl()
    {
        float ad_input = Input.GetAxisRaw("Horizontal"); 
        float ws_input = Input.GetAxisRaw("Vertical");
        if (ad_input != 0 && ws_input != 0 && hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.CompareTag("Ground"))
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