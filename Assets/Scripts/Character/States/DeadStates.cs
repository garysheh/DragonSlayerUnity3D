using UnityEngine;
using UnityEngine.AI;

public class DeadState : EnemyStates
{
    private EnemyControllerWithFSM controller;
    private readonly NavMeshAgent agent;
    private readonly Animator animator;
    private Collider collider;

    public DeadState(EnemyControllerWithFSM controller, NavMeshAgent agent, Animator animator, Collider collider)
    {
        this.controller = controller;
        this.agent = agent;
        this.animator = animator;
        this.collider = collider;
    }

    public void Tick()
    {
        Object.Destroy(controller.gameObject, 2f);
    }

    public void OnEnter()
    {
        animator.SetBool("Dead", true);
        collider.enabled = false;
        agent.isStopped = true;
        agent.radius = 0f;
    }
    public void OnExit()
    {

    }
}