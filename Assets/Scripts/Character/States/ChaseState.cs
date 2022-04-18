using UnityEngine;
using UnityEngine.AI;

public class ChaseState : EnemyStates
{
    private readonly NavMeshAgent agent;
    private readonly Animator animator;
    private readonly GameObject attackTarget;

    public ChaseState(NavMeshAgent agent, Animator animator, GameObject attackTarget)
    {
        this.agent = agent;
        this.animator = animator;
        this.attackTarget = attackTarget;
    }

    public void Tick()
    {
        animator.SetBool("Follow", true);
        agent.isStopped = false;
        agent.destination = attackTarget.transform.position;
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