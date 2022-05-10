using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : Singleton<cameraController>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
}
