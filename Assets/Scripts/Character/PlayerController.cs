using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // create nav mesh agent class

public class PlayerController : MonoBehaviour
{
    
    private NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        MouseController.Instance.OnMouseClicked += MoveToTarget;
    }

    // function that gets the coordinate of character then transport to the agent
    // destination
    public void MoveToTarget(Vector3 target)
    {
        agent.destination = target;
    }
}
