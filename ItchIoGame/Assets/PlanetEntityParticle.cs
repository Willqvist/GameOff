using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetEntityParticle : MonoBehaviour
{
    public PlanetEntity planetEntity;
    public ParticleSystem ps;

    public void Update()
    {
        if(planetEntity.isRunning)
        {
            this.ps.gameObject.SetActive(true);
        }
        else
        {
            this.ps.gameObject.SetActive(false);
        }
    }
}
