using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // create nav mesh agent class

public class PlayerController : MonoBehaviour
{
    
    private NavMeshAgent agent;
    private Animator animator;
    private CharacterStats characterStats; // character status
    private GameObject attackEnemy; // object to attack
    private float cd; // normal attack cooldown


    void Update()
    {
        AnimationControl();
        cd -= Time.deltaTime;
    }

    void AnimationControl()
    {
        animator.SetFloat("Speed",agent.velocity.sqrMagnitude);
    }
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();
    }


    void Start()
    {
        MouseController.Instance.OnMouseClicked += MoveToTarget;
        MouseController.Instance.OnEnemyClciked += AttackEnemy;
    }

    private void SwitchAnimation()
    {
        animator.SetFloat("Speed", agent.velocity.sqrMagnitude);
    }

    // function that gets the coordinate of character then transport to the agent
    // destination
    public void MoveToTarget(Vector3 target)
    {
        StopAllCoroutines();
        agent.isStopped = false;
        agent.destination = target;
    }

    private void AttackEnemy(GameObject target)
    {
        if (target != null)
        {
            attackEnemy = target;
            characterStats.isCrit = UnityEngine.Random.value < characterStats.attackData.critChance;
            StartCoroutine(MoveToEnemy());
        }
    }

    IEnumerator MoveToEnemy()
    {
        agent.isStopped = false; // to make sure the character keeps chasing to the enemy
        transform.LookAt(attackEnemy.transform);
        while(Vector3.Distance(attackEnemy.transform.position, transform.position) > 1)
        {
            agent.destination = attackEnemy.transform.position;
            yield return null;
        }
        // when close to the enemy, character stops
        agent.isStopped = true;
        // attack part
        if (cd < 0)
        {
            animator.SetBool("Critical", characterStats.isCrit);
            animator.SetTrigger("Attack");
            // refresh cooldown
            cd = characterStats.attackData.attackCD;
        }
    }

    void attack()
    {
        var EnemyStats = attackEnemy.GetComponent<CharacterStats>();
        EnemyStats.takeDamage(characterStats, EnemyStats);
    }
}
