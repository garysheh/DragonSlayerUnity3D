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
        controller.skillCD -= Time.deltaTime;
        controller.transform.LookAt(attackTarget.transform);

        if (controller.skillCD < 0 && TargetInSkillRange())
        {
            animator.SetTrigger("Skill");
            controller.skillCD = enemyStats.SkillCD;
        }
        else if (controller.attackCD <= 0)
        {
            if (!TargetInAttackRange())
            {
                agent.isStopped = false;
                animator.SetBool("Follow", true);
                agent.SetDestination(attackTarget.transform.position);
            }
            else
            {
                agent.isStopped = true;
                animator.SetBool("Follow", false);
                //  refresh attack cd
                controller.attackCD = enemyStats.AttackCD;
                controller.CriticalCheck();
                animator.SetBool("Critical", enemyStats.isCrit);
                animator.SetTrigger("Attack");
                //targetStats.takeDamage(enemyStats, targetStats);
            }
        }
    }

    bool TargetInAttackRange()
    {
        return controller.DistanceFromTarget() <= enemyStats.attackData.attackRange;
            
    }

    bool TargetInSkillRange()
    {
        return controller.DistanceFromTarget() <= enemyStats.attackData.skillRange;
    }


    public void OnEnter()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Follow", true);
        animator.SetBool("Chase", true);
        agent.isStopped = true;
    }
    public void OnExit()
    {
        agent.isStopped = false;
    }
}