using UnityEngine;
using UnityEngine.AI;

public class PatrolState : EnemyStates
{
    private readonly EnemyControllerWithFSM controller;
    private readonly NavMeshAgent agent;
    private readonly Animator animator;
    Vector3 wayPoint;
    float remainGapTime;

    public PatrolState(EnemyControllerWithFSM controller, NavMeshAgent agent, Animator animator)
    {
        this.controller = controller;
        this.agent = agent;
        this.animator = animator;
        wayPoint = controller.GetNewWayPoint();
        remainGapTime = controller.patrolGapTime;
    }

    public void Tick()
    {
        Debug.DrawLine(controller.transform.position, wayPoint);
        //  check if waypoint reached
        if (controller.IsPointReached(wayPoint))
        {
            animator.SetBool("Walk", false);
            //  stay at current position until gap time pass
            if (remainGapTime > 0)
            {
                remainGapTime -= Time.deltaTime;
            }
            else
            {
                remainGapTime = controller.patrolGapTime;
                wayPoint = controller.GetNewWayPoint();
            }
        }
        else
        {
            MoveToWayPoint();
        }
    }

    void MoveToWayPoint()
    {
        animator.SetBool("Walk", true);
        agent.destination = wayPoint;
    }
    public void OnEnter()
    {
        agent.speed *= 0.5f;
    }
    public void OnExit()
    {
        agent.speed *= 2.0f;
    }
}