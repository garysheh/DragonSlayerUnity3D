using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : Singleton<GameManager>
{
     
    public CharacterStats playerStats;

    private CinemachineFreeLook freeLook;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void RigisterPlayer(CharacterStats player)
    {
        playerStats = player;

        freeLook = FindObjectOfType<CinemachineFreeLook>();
        if(freeLook != null)
        {
            freeLook.Follow = playerStats.transform.GetChild(3);
            freeLook.LookAt = playerStats.transform.GetChild(3);
        } 
    }
}
