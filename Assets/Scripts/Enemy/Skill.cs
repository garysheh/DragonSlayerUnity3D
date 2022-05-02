using UnityEngine;
using UnityEngine.AI;

public class Skill : MonoBehaviour
{
    protected EnemyControllerWithFSM controller;

    [Header("Skill")]
    public float force = 10f;
    public int damage;

    private void Awake()
    {
        controller = GetComponent<EnemyControllerWithFSM>();
    }

    public void KnockOff()
    {
        if (transform.IsTargetInfront(controller.attackTarget.transform)
            && controller.DistanceFromTarget() <= controller.GetComponent<CharacterStats>().AttackRange)
        {
            transform.LookAt(controller.attackTarget.transform);

            Vector3 direction = controller.attackTarget.transform.position - transform.position;
            direction.Normalize();

            NavMeshAgent targetAgent = controller.attackTarget.GetComponent<NavMeshAgent>();

            targetAgent.isStopped = false;
            targetAgent.ResetPath();
            targetAgent.velocity = direction * force;

            CharacterStats targetStats = controller.attackTarget.GetComponent<CharacterStats>();

            targetStats.takeDamage(this.GetComponent<CharacterStats>(), targetStats, damage);
        }
    }
}
