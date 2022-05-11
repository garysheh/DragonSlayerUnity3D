using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthShatter : Skill
{
    public GameObject earth;
    public ParticleSystem earthPS;

    private void Start()
    {
        earthPS = earth.GetComponent<ParticleSystem>();
    }

    public void ShatterOn()
    {
        earth.SetActive(true);
    }

    public void ShatterOff()
    {
        earth.SetActive(false);
    }
}
