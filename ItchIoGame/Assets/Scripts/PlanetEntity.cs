using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetEntity : MonoBehaviour
{
    public Planet planet;

    public virtual void Update()
    {
        if(this.planet == null)
        {
            return;
        }

        this.planet.Place(this.transform);
    }

    public virtual void PlaceOnPlanet(Planet planet)
    {
        this.planet = planet;
    }

    public void RotateTowardsPlanet(Planet planet)
    {
        Vector3 dir = (this.transform.position - planet.transform.position).normalized;
        this.transform.rotation = Quaternion.FromToRotation(this.transform.up, dir) * this.transform.rotation;
    }
}
