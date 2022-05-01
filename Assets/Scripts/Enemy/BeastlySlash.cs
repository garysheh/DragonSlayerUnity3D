using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeastlySlash : MonoBehaviour
{
    private EnemyControllerWithFSM controller;

    [Header("Skill")]
    public float force = 10f;

    private void Awake()
    {
        controller = GetComponent<EnemyControllerWithFSM>();
    }

    public void Slash()
    {
        if (controller.attackTarget != null)
        {
            transform.LookAt(controller.attackTarget.transform);

            Vector3 direction = controller.attackTarget.transform.position - transform.position;
            direction.Normalize();

            controller.attackTarget.GetComponent<NavMeshAgent>().isStopped = true;
            controller.attackTarget.GetComponent<NavMeshAgent>().velocity = direction * force;

            Debug.Log("-100");
        }
    }

}
