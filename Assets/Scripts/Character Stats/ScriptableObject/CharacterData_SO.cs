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
}
