using UnityEngine;
using UnityEngine.AI;

public class ChaseState : EnemyStates
{
    private readonly EnemyControllerWithFSM controller;
    private readonly NavMeshAgent agent;
    private readonly Animator animator;
    private readonly GameObject attackTarget;
    public ChaseState(NavMeshAgent agent, Animator animator, GameObject target)
    {
        this.agent = agent;
        this.animator = animator;
        this.attackTarget = target;
    }

    public void Tick()
    {
        animator.SetBool("Follow", true);
        agent.isStopped = false;
        agent.SetDestination(attackTarget.transform.position);
    }
    
    public void OnEnter()
    {
        animator.SetBool("Chase", true);
    }
    public void OnExit()
    {
        animator.SetBool("Chase", false);
        animator.SetBool("Follow", false);
    }
}