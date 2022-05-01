using UnityEngine;
using UnityEngine.AI;

public class BeastlySlash : Skill
{
    public void Slash()
    {
        if (transform.IsTargetInfront(controller.attackTarget.transform))
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
        if (transform.IsTargetInfront(controller.attackTarget.transform))
        {
            transform.LookAt(controller.attackTarget.transform);
            controller.attackTarget.GetComponent<NavMeshAgent>().isStopped = true;
            controller.attackTarget.GetComponent<Animator>().SetTrigger("Dizzy");
            targetStats.isCrit = false;
            targetStats.takeDamage(this.GetComponent<CharacterStats>(), targetStats);
        }
    }

}
