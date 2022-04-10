using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState { GUARD, PATROL, CHASE, DEAD }
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private EnemyState enemyState;
    private Animator anim;

    [Header("Basic Settings")]
    public float sightRadius;
    public bool isGuard;
    private GameObject attackTarget;
    private float speed;

    [Header("Patrol Settings")]
    public float patrolRadius;
    public float patrolGapTime;

    private Vector3 wayPoint;
    private Vector3 refreshPoint;
    private float remainGapTime;


    //  Animator parameters
    bool isWalk;
    bool isChase;
    bool isFollow;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        speed = agent.speed;
        refreshPoint = transform.position;
        remainGapTime = patrolGapTime;

        if (isGuard)
        {
            enemyState = EnemyState.GUARD;
        }
        else
        {
            enemyState = EnemyState.PATROL;
            GetNewWayPoint();
        }
    }

    // Update is called once per frame
    void Update()
    {
        SwitchState();
        SwitchAnimation();
    }

    void SwitchAnimation()
    {
        anim.SetBool("Walk", isWalk);
        anim.SetBool("Chase", isChase);
        anim.SetBool("Follow", isFollow);
    }

    void SwitchState()
    {
        if (FoundPlayer())
        {
            enemyState = EnemyState.CHASE;
        }

        switch (enemyState)
        {
            case EnemyState.GUARD:
                break;
            case EnemyState.PATROL:
                Patrol();
                break;
            case EnemyState.CHASE:
                Chase();
                break;
            case EnemyState.DEAD:
                break;
            default:
                break;
        }
    }

    bool FoundPlayer()
    {
        var colliders = Physics.OverlapSphere(transform.position, sightRadius);
        bool foundPlayer = false;
        foreach (var target in colliders)
        {
            if (target.CompareTag("Player"))
            {
                attackTarget = target.gameObject;
                Debug.Log(attackTarget + "found");
                foundPlayer = true;
            }
        }
        return foundPlayer;
    }
    #region Chase
    void Chase()
    {
        isWalk = false;
        isChase = true;
        if (!FoundPlayer())
        {
            BackToLastState();
        }
        else
        {
            FollowPlayer();
        }
    }
    
    void FollowPlayer()
    {
        isFollow = true;
        agent.speed = speed;
        agent.destination = attackTarget.transform.position;
    }

    void BackToLastState()
    {
        isFollow = false;
        attackTarget = null;
        if (remainGapTime > 0)
        {
            agent.destination = transform.position;
            remainGapTime -= Time.deltaTime;
        }
        else if (isGuard)
        {
            enemyState = EnemyState.GUARD;
            agent.destination = refreshPoint;
        }
        else
        {
            enemyState = EnemyState.PATROL;
        }
       
    }
    #endregion

    #region Patrol
    void Patrol()
    {
        isChase = false;
        //  enemy don't patrol at full speed
        agent.speed = speed * 0.5f;

        Debug.DrawLine(transform.position, wayPoint);
        //  check if waypoint reached
        if (Vector3.Distance(wayPoint, transform.position) <= agent.stoppingDistance)
        {
            isWalk = false;
            if (remainGapTime > 0)
            {
                remainGapTime -= Time.deltaTime;
            }
            else
            {
                remainGapTime = patrolGapTime;
                GetNewWayPoint();
            }
            }
        else
        {
            isWalk = true;
            agent.destination = wayPoint;
        }
    }


    void GetNewWayPoint()
    {
        float randomX = Random.Range(-patrolRadius, patrolRadius);
        float randomZ = Random.Range(-patrolRadius, patrolRadius);

        Vector3 randomPoint = new Vector3(refreshPoint.x + randomX,
                                        transform.position.y,
                                        refreshPoint.z + randomZ);
        NavMeshHit hit;
        wayPoint = (NavMesh.SamplePosition(randomPoint, out hit, patrolRadius, 1)) ? hit.position : transform.position;
    }
    #endregion
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(refreshPoint, sightRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(refreshPoint, patrolRadius);
    }
}
