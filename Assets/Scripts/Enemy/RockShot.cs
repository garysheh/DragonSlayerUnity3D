using UnityEngine;
using UnityEngine.AI;

public class RockShot : Skill
{
    public GameObject[] rocks = new GameObject[3];
    public Transform handPosition;

    public void Rock_Shot()
    {
        int index = Random.Range(0, 3);
        var rock = Instantiate(rocks[index], handPosition.position, Quaternion.identity);
        rock.GetComponent<Rock>().FlyToTarget(damage);
        GetComponent<NavMeshAgent>().SetDestination(GetComponent<EnemyControllerWithFSM>().attackTarget.transform.position);
    }
}
