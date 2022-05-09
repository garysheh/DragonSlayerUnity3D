using UnityEngine;
using UnityEngine.AI;

public class WinState : EnemyStates
{
    private readonly EnemyControllerWithFSM controller;
    private readonly NavMeshAgent agent;
    private readonly Animator animator;

    public WinState(EnemyControllerWithFSM controller, NavMeshAgent agent, Animator animator)
    {
        this.controller = controller;
        this.agent = agent;
        this.animator = animator;
    }

    public void Tick()
    {
        controller.gameOver = true;
        controller.attackTarget = null;
    }

    public void OnEnter()
    {
        agent.isStopped = true;
        animator.SetBool("Walk", false);
        animator.SetBool("Chase", false);
        animator.SetBool("Follow", false);
        animator.SetBool("Win", true);
    }


    public void OnExit()
    {
       
    }

}
