using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyControllerWithFSM : MonoBehaviour
{
    //  State Machine
    private FSM enemyFSM;
    private GuardState guardState;
    private PatrolState patrolState;
    private ChaseState chaseState;
    private CombatState combatState;
    private DeadState deadState;

    private NavMeshAgent agent;
    private EnemyState enemyState;
    private Animator anim;
    private CharacterStats enemyStats;

    [Header("Basic Settings")]
    public float sightRadius;
    public bool isGuard;
    private GameObject attackTarget;
    private float speed;

    [Header("Patrol Settings")]
    public float patrolRadius;
    public float patrolGapTime;
    private Vector3 wayPoint;
    private float remainGapTime;

    //  guard info
    private Transform refreshTransform;
    

    //  attack timer
    private float attackCD;
    //  Animator parameters
    bool isWalk;
    bool isChase;
    bool isFollow;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        enemyStats = GetComponent<CharacterStats>();

        refreshTransform = transform;

        #region State Machine Initialization
        enemyFSM = new FSM();
        guardState = new GuardState(this, agent, anim, refreshTransform);
        patrolState = new PatrolState(this, agent, anim);
        chaseState = new ChaseState(agent, anim, attackTarget);
        combatState = new CombatState(this);
        deadState = new DeadState(this);
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
        Func<bool> IsDead() => () => IsHealthZero();
        
    } 

    // Start is called before the first frame update
    void Start()
    {
        speed = agent.speed;
        remainGapTime = patrolGapTime;
    }

    // Update is called once per frame
    void Update()
    {
        enemyFSM.Tick();
    }

    bool IsHealthZero()
    { 
        //  when enemy is dead return 0;
        return enemyStats.CurrentHealth == 0;
    }

    float DistanceFromTarget()
    {
        if (attackTarget != null)
        {
            return Vector3.Distance(attackTarget.transform.position, transform.position);
        }
        return Mathf.Infinity;
    }

    float MaxCombatRange()
    {
        if (enemyStats.SkillRange != null)
        {

            Mathf.Max(enemyStats.AttackRange, enemyStats.SkillRange.Max()); return enemyStats.AttackRange;
        }
        return Mathf.Max(enemyStats.AttackRange, enemyStats.SkillRange.Max());
    }

    public bool FoundPlayer()
    {
        var colliders = Physics.OverlapSphere(transform.position, sightRadius);
        bool foundPlayer = false;
        foreach (var target in colliders)
        {
            if (target.CompareTag("Player"))
            {
                attackTarget = target.gameObject;
                foundPlayer = true;
            }
        }
        return foundPlayer;
    }

    public Vector3 GetNewWayPoint()
    {
        float randomX = UnityEngine.Random.Range(-patrolRadius, patrolRadius);
        float randomZ = UnityEngine.Random.Range(-patrolRadius, patrolRadius);

        Vector3 randomPoint = new Vector3(refreshTransform.position.x + randomX,
                                        transform.position.y,
                                        refreshTransform.position.z + randomZ);
        NavMeshHit hit;
        return (NavMesh.SamplePosition(randomPoint, out hit, patrolRadius, 1)) ?
            hit.position :
            transform.position;
    }

    public bool IsPointReached(Vector3 point)
    {
        return Vector3.SqrMagnitude(point - transform.position) <= agent.stoppingDistance;
    }

    bool TargetInAttackRange()
    {
        return (attackTarget != null) ?
            Vector3.Distance(attackTarget.transform.position, transform.position) <= enemyStats.attackData.attackRange :
            false;
    }


    void AttackPlayer()
    {
        transform.LookAt(attackTarget.transform);
        if (attackCD < 0)
        {
            //  refresh attack cd
            attackCD = enemyStats.attackData.attackCD;
            if (TargetInAttackRange())
            {
                CriticalCheck();
                anim.SetTrigger("Attack");
            }

        }
    }

    //  animation event
    void Attack()
    {
        if (attackTarget != null)
        {
            CharacterStats targetStats = attackTarget.GetComponent<CharacterStats>();
            targetStats.takeDamage(enemyStats, targetStats);
        }
    }


    void CriticalCheck()
    {
        enemyStats.isCrit = UnityEngine.Random.value < enemyStats.attackData.critChance;
    }

    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(refreshTransform.position, sightRadius);
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(refreshTransform.position, patrolRadius);
    //}
}
