using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class SceneController : Singleton<SceneController>
{
    GameObject player;

    NavMeshAgent playerAgent;

    public void Transport(TransitionPoint transitionPoint)
    {
        switch(transitionPoint.transitionType)
        {
            case TransitionPoint.TransitionType.SameScene:
                StartCoroutine(Transition(SceneManager.GetActiveScene().name, transitionPoint.destinationTag));
                break;
            case TransitionPoint.TransitionType.DifferetScene:
                break; 
        }
    }

    IEnumerator Transition(string sceneName, TransitionDestination.DestinationTag destinationTag)
    {
        player = GameManager.Instance.playerStats.gameObject;
        playerAgent = player.GetComponent<NavMeshAgent>();
        playerAgent.enabled = false;
        player.transform.SetPositionAndRotation(GetDestination(destinationTag).transform.position, GetDestination(destinationTag).transform.rotation);
        playerAgent.enabled = true;
        yield return null;
    }

    public TransitionDestination GetDestination(TransitionDestination.DestinationTag destinationTag)
    {
        var entry = FindObjectsOfType<TransitionDestination>();

        for (int i = 0; i < entry.Length; i++)
        {
            if (entry[i].destinationTag == destinationTag)
            {
                return entry[i];
            }
        }
           return null;
    }
}
