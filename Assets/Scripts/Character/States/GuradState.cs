using UnityEngine;
using UnityEngine.AI;

public class GuardState : EnemyStates
{
    private readonly EnemyControllerWithFSM controller;
    private readonly NavMeshAgent agent;
    private readonly Animator animator;
    private Vector3 refreshPoint;
    private Quaternion refreshRotation;

    public GuardState(EnemyControllerWithFSM controller, NavMeshAgent agent, Animator animator, Transform transform)
    {
        
        refreshPoint = transform.position;
        refreshRotation = transform.rotation;
    }

    public void Tick()
    {
        if (controller.IsPointReached(refreshPoint))
        {
            agent.destination = controller.transform.position;
            controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, refreshRotation, 0.05f);
            animator.SetBool("Walk", false);
        }
        else
        {
            animator.SetBool("Walk", true);
            agent.isStopped = false;
            agent.destination = refreshPoint;
        }
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
        animator.SetBool("Walk", false);
    }
}