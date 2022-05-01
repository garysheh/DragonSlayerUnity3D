using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    protected EnemyControllerWithFSM controller;

    [Header("Skill")]
    public float force = 100f;
    public int damage;
    protected CharacterStats targetStats;

    private void Awake()
    {
        controller = GetComponent<EnemyControllerWithFSM>();
        targetStats = controller.attackTarget.GetComponent<CharacterStats>();
    }
}
