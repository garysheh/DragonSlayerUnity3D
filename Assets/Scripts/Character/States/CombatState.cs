using UnityEngine;
using UnityEngine.AI;

public class CombatState : EnemyStates
{
    private readonly EnemyControllerWithFSM controller;
    private readonly NavMeshAgent agent;
    private readonly Animator animator;
    private CharacterStats enemyStats;
    private readonly GameObject attackTarget;
    private CharacterStats targetStats;

    public CombatState(EnemyControllerWithFSM controller, NavMeshAgent agent, Animator animator, CharacterStats enemyStats, GameObject target)
    {
        this.controller = controller;
        this.agent = agent;
        this.animator = animator;
        this.enemyStats = enemyStats;
        this.attackTarget = target;
        this.targetStats = attackTarget.GetComponent<CharacterStats>();
    }

    public void Tick()
    {
        controller.transform.LookAt(attackTarget.transform);
        if (controller.attackCD < 0)
        {
            //  refresh attack cd
            controller.attackCD = enemyStats.attackData.attackCD;
            CriticalCheck();
            animator.SetBool("Critical", enemyStats.isCrit);
            animator.SetTrigger("Attack");
            targetStats.takeDamage(enemyStats, targetStats);
        }
    }

    void CriticalCheck()
    {
        enemyStats.isCrit = Random.value < enemyStats.attackData.critChance;
    }

    public void OnEnter()
    {
        agent.isStopped = true;
    }
    public void OnExit()
    {
        agent.isStopped = false;
    }
}