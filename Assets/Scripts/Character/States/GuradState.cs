using UnityEngine;
using UnityEngine.AI;

public class GuardState : EnemyStates
{
    private readonly EnemyControllerWithFSM controller;
    private readonly NavMeshAgent agent;
    private readonly Animator animator;
    private Vector3 refreshPoint;
    private Quaternion refreshRotation;

    public GuardState(EnemyControllerWithFSM controller, NavMeshAgent agent, Animator animator, Vector3 refreshPoint, Quaternion refreshRotation)
    {
        this.controller = controller;
        this.agent = agent;
        this.animator = animator;
        this.refreshPoint = refreshPoint;
        this.refreshRotation = refreshRotation;
    }

    public void Tick()
    {
        if (IsPointReached(refreshPoint))
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

    private bool IsPointReached(Vector3 point)
    {
        return Vector3.SqrMagnitude(point - controller.transform.position) <= agent.stoppingDistance;
    }

    public void OnEnter()
    {
        animator.SetBool("Walk", false);
    }

    public void OnExit()
    {
        animator.SetBool("Walk", false);
    }
}