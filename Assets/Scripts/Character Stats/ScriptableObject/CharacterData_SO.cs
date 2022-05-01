using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "Character Data/Data")]
public class CharacterData_SO : ScriptableObject
{
    [Header("Basic Stats")]
    public int maxHealth;
    public int currentHealth;
    public int maxMana;
    public int currentMana;
    public int baseDefence;
    public int currentDefence;

    [Header("Level")]
    public int curlevel; // current level;
    public int topLevel; // maximum level;
    public int expBase; // exp base
    public int curExp; // current exp
    public float levelBuff;

    public float levelMultiplier
    {
        get
        {
            return (curlevel - 1) * levelBuff + 1;
        }
    }

    public void getExp(int exp)
    {
        curExp += exp;

        if (curExp >= expBase)
        {
            leveling();
        }
    }

    void leveling()
    {
        curlevel = Mathf.Clamp(curlevel + 1, 0, topLevel); // to distingush between level 0 to the maximum level;
        expBase += (int)(expBase * levelMultiplier);
        maxHealth = (int)(maxHealth * levelMultiplier);

    }
}
