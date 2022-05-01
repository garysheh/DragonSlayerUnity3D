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
    [HideInInspector]
    public Vector3 refreshPoint;
    private Quaternion refreshRotation;

    //  attack timer
    //[HideInInspector]
    public float attackCD;
    //[HideInInspector]
    public float skillCD;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        enemyStats = GetComponent<CharacterStats>();
        coll = GetComponent<Collider>();
        attackTarget = GameObject.FindGameObjectWithTag("Player");

        refreshPoint = transform.position;
        skillCD = enemyStats.SkillCD;

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
        return Vector3.Distance(attackTarget.transform.position, transform.position);
    }

    public float MaxCombatRange()
    {
        return Mathf.Max(enemyStats.AttackRange, enemyStats.SkillRange);    
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

    //  animation event
    void Attack()
    {
        if (attackTarget != null)
        {
            CharacterStats targetStats = attackTarget.GetComponent<CharacterStats>();
            CriticalCheck();
            targetStats.takeDamage(enemyStats, targetStats);
        }
    }

    public void CriticalCheck()
    {
        enemyStats.isCrit = UnityEngine.Random.value < enemyStats.attackData.critChance;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(refreshPoint, patrolRadius);
    }
}
