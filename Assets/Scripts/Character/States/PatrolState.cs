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
        wayPoint = GetNewWayPoint();
        remainGapTime = controller.patrolGapTime;
    }

    public void Tick()
    {
        //Debug.Log("in patrol tick");
        Debug.DrawLine(controller.transform.position, wayPoint);
        //  check if waypoint reached
        if (IsPointReached(wayPoint))
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
                wayPoint = GetNewWayPoint();
            }
        }
        else
        {
            MoveToWayPoint();

        }
    }

    private bool IsPointReached(Vector3 point)
    {
        return Vector3.SqrMagnitude(point - controller.transform.position) <= agent.stoppingDistance;
    }

    private Vector3 GetNewWayPoint()
    {
        float randomX = UnityEngine.Random.Range(-controller.patrolRadius, controller.patrolRadius);
        float randomZ = UnityEngine.Random.Range(-controller.patrolRadius, controller.patrolRadius);
        Vector3 randomPoint = new Vector3(controller.refreshPoint.x + randomX,
                                        controller.transform.position.y,
                                        controller.refreshPoint.z + randomZ);
        NavMeshHit hit;

        var point = (NavMesh.SamplePosition(randomPoint, out hit, controller.patrolRadius, 1)) ?
            hit.position :
            controller.transform.position;
        return point;
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