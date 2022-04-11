using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "Character Data/Data")]
public class CharacterData_SO : ScriptableObject
{
    #region from Data_SO
    [Header("Basic Stats")]
    public int maxHealth;
    public int currentHealth;
    public int baseDefence;
    public int currentDefence;
    #endregion

}
