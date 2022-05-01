using UnityEngine;
using UnityEngine.AI;

public class BeastlySlash : MonoBehaviour
{
    private EnemyControllerWithFSM controller;

    [Header("Skill")]
    public float force = 100f;
    public int damage;
    private CharacterStats targetStats;

    private void Awake()
    {
        controller = GetComponent<EnemyControllerWithFSM>();
        targetStats = controller.attackTarget.GetComponent<CharacterStats>();
    }

    public void Slash()
    {
        if (controller.attackTarget != null)
        {
            transform.LookAt(controller.attackTarget.transform);

            Vector3 direction = controller.attackTarget.transform.position - transform.position;
            direction.Normalize();

            controller.attackTarget.GetComponent<NavMeshAgent>().isStopped = false;
            controller.attackTarget.GetComponent<NavMeshAgent>().velocity = direction * force;

            CharacterStats targetStats = controller.attackTarget.GetComponent<CharacterStats>();

            targetStats.takeDamage(this.GetComponent<CharacterStats>(), targetStats, damage);
        }
    }

    public void Stun()
    {
        transform.LookAt(controller.attackTarget.transform);
        controller.attackTarget.GetComponent<NavMeshAgent>().isStopped = true;
        controller.attackTarget.GetComponent<Animator>().SetTrigger("Dizzy");
        targetStats.isCrit = false;
        targetStats.takeDamage(this.GetComponent<CharacterStats>(), targetStats);
    }

}
