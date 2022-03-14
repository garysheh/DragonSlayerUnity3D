using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamDemo : MonoBehaviour
{
    public GameObject objectToFollow; // object to collect for camera
    public float speed = 2.0f; // mov speed
    public Vector3 offset = new Vector3(0,5,10); // distance from the object of third person view

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, objectToFollow.transform.position + offset, speed*Time.deltaTime);
        
    }
}
