using UnityEngine;
using UnityEngine.AI;

public class BeastlySlash : Skill
{
    public void Stun()
    {
        if (transform.IsTargetInfront(controller.attackTarget.transform))
        {
            transform.LookAt(controller.attackTarget.transform);
            controller.attackTarget.GetComponent<NavMeshAgent>().isStopped = true;
            controller.attackTarget.GetComponent<Animator>().SetTrigger("Dizzy");

            CharacterStats targetStats = controller.attackTarget.GetComponent<CharacterStats>();

            targetStats.isCrit = false;
            targetStats.TakeDamage(this.GetComponent<CharacterStats>(), targetStats);
        }
    }

}
