using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterStats))]
public class EnemyControllerWithFSM : MonoBehaviour
{
    //  State Machine
    private FSM enemyFSM;
    //private GuardState guardState;
    //private PatrolState patrolState;
    //private ChaseState chaseState;
    //private CombatState combatState;
    //private DeadState deadState;

    private NavMeshAgent agent;
    private Animator anim;
    private CharacterStats enemyStats;
    private Collider coll;

    [Header("Basic Settings")]
    public float sightRadius;
    public bool isGuard;
    public GameObject attackTarget;

    [Header("Patrol Settings")]
    public float patrolRadius;
    public float patrolGapTime;

    //  guard info
    private Vector3 refreshPoint;
    private Quaternion refreshRotation;

    //  attack timer
    [HideInInspector]
    public float attackCD;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        enemyStats = GetComponent<CharacterStats>();
        coll = GetComponent<Collider>();
        attackTarget = GameObject.FindGameObjectWithTag("Player");

        refreshPoint = transform.position;
        #region State Machine Initialization
        enemyFSM = new FSM();
        var guardState = new GuardState(this, agent, anim, refreshPoint, refreshRotation);
        var patrolState = new PatrolState(this, agent, anim);
        var chaseState = new ChaseState(agent, anim, attackTarget);
        var combatState = new CombatState(this, agent, anim, enemyStats, attackTarget);
        var deadState = new DeadState(this, agent, anim, coll);
        Debug.Log("all states have been initialized");
        //  when(a, b, c); when "a" is valid go from b to c;
        When(HasTarget(), guardState, chaseState);
        When(HasTarget(), patrolState, chaseState);
        When(TargetInRange(), chaseState, combatState);
        When(TargetOutOfRange(), combatState, chaseState);
        When(LostTargetAndWasGuard(), chaseState, guardState);
        When(LostTargetAndWasPatrol(), chaseState, patrolState);
        enemyFSM.AddAnyTransition(IsDead(), deadState);

        if (isGuard)
        {
            enemyFSM.SetState(guardState);
        }
        else
        {
            enemyFSM.SetState(patrolState);
        }
        #endregion

        void When(Func<bool> condition, EnemyStates from, EnemyStates to) => enemyFSM.AddTransition(condition, from, to);
        Func<bool> HasTarget() => () => FoundPlayer();
        Func<bool> TargetInRange() => () => MaxCombatRange() >= DistanceFromTarget();
        Func<bool> TargetOutOfRange() => () => MaxCombatRange() < DistanceFromTarget();
        Func<bool> LostTargetAndWasGuard() => () => !FoundPlayer() && (isGuard == true);
        Func<bool> LostTargetAndWasPatrol() => () => !FoundPlayer() && (isGuard == false);
        Func<bool> IsDead() => () => IsHealthZero() == true;
    } 

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //  attack timer
        attackCD -= Time.deltaTime;
        enemyFSM.Tick();
    }

    public bool IsHealthZero()
    { 
        //  when enemy is dead return 0;
        return enemyStats.CurrentHealth == 0;
    }

    public float DistanceFromTarget()
    {
        if (attackTarget != null)
        {
            return Vector3.Distance(attackTarget.transform.position, transform.position);
        }
        return Mathf.Infinity;
    }

    public float MaxCombatRange()
    {
        if (enemyStats.SkillRange != null)
        {
            return Mathf.Max(enemyStats.AttackRange, enemyStats.SkillRange.Max()); 
        }
        return enemyStats.AttackRange;
    }

    public bool FoundPlayer()
    {
        var colliders = Physics.OverlapSphere(transform.position, sightRadius);
        bool foundPlayer = false;
        foreach (var target in colliders)
        {
            if (target.CompareTag("Player"))
            {
                foundPlayer = true;
            }
        }
        return foundPlayer;
    }

    public Vector3 GetNewWayPoint()
    {
        float randomX = UnityEngine.Random.Range(-patrolRadius, patrolRadius);
        float randomZ = UnityEngine.Random.Range(-patrolRadius, patrolRadius);
        Vector3 randomPoint = new Vector3(refreshPoint.x + randomX,
                                        transform.position.y,
                                        refreshPoint.z + randomZ);
        NavMeshHit hit;

        var point =  (NavMesh.SamplePosition(randomPoint, out hit, patrolRadius, 1)) ?
            hit.position :
            transform.position;
        return point;
    }

    public bool IsPointReached(Vector3 point)
    {
        return Vector3.SqrMagnitude(point - transform.position) <= agent.stoppingDistance;
    }

    public bool TargetInAttackRange()
    {
        return (attackTarget != null) ?
            Vector3.Distance(attackTarget.transform.position, transform.position) <= enemyStats.attackData.attackRange :
            false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(refreshPoint, patrolRadius);
    }
}
