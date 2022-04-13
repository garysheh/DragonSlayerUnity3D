using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skills", menuName = "Attack/Skill Data")]
public class SkillData_SO : ScriptableObject
{
    //  these four arrays are in 1-1-1-1 relation
    public string[] skillName = new string[4];
    public int[] manaCost = new int[4];
    public int[] skillDamage = new int[4];
    public int[] skillCD = new int[4];
}
