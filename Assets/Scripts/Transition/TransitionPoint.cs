using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPoint : MonoBehaviour
{
    public enum TransitionType
    {
        SameScene, DifferetScene
    }

    [Header("Transport")]
    public string sceneName;
    public TransitionType transitionType;

    public TransitionDestination.DestinationTag destinationTag;

    public bool transON;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && transON)
        {
            SceneController.Instance.Transport(this);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            transON = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transON = false;
        }
    }

}
