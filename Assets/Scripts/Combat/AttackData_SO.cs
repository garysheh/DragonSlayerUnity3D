using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attack/Attack Data")]
public class AttackData_SO : ScriptableObject
{
    public float attackRange;
    public float attackCD;

    //  normal attack
    public int minDamage;
    public int maxDamage;

    //  skill attack
    public float skillRange;
    public float skillCD;

    //  critical attack
    public float critMultiplier;
    public float critChance;
}
