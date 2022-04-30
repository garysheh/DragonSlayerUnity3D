using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CharacterStats plaerStats;

    public void RigisterPlayer(CharacterStats player)
    {
        plaerStats = player;
    }
}
