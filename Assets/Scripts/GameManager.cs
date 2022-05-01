using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
     
    public CharacterStats plaerStats;

    public void RigisterPlayer(CharacterStats player)
    {
        plaerStats = player;
    }
}
