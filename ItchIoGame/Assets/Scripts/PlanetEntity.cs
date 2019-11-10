using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetEntity : MonoBehaviour {

    public Planet planet;
    public string entityName;
    public float colliderHeight = 2f;
    public Resource electricityResource;
    public Resource populationResource;

    public virtual void Start()
    {
        /*
        CapsuleCollider cap = this.GetComponent<CapsuleCollider>();
        if (cap != null)
        {
            this.colliderHeight = cap.height;
            Debug.Log("height: " + this.colliderHeight);
            return;
        }
        */
    }
    public virtual void Update()
    {
        if(this.planet == null)
        {
            return;
        }

        //this.planet.Place(this.transform);
    }

    private void OnDestroy()
    {
        Planet.Current.electricity -= this.electricityResource.value;
        UiManager.Instance.DecreaseResource(this.electricityResource, this.electricityResource.value);
    }

    public virtual void PlaceOnPlanet(Planet planet,Vector3 position)
    {
        this.planet = planet;
        this.transform.position = position;

        Planet.Current.electricity += this.electricityResource.value;
        Planet.Current.ActiveWorkers += -this.populationResource.value;
        UiManager.Instance.IncreaseResource(this.electricityResource, this.electricityResource.value);
    }

    public float getColliderHeight()
    {
        return this.colliderHeight;
    }


    public void RotateTowardsPlanet(Planet planet)
    {
        Vector3 dir = (this.transform.position - planet.transform.position).normalized;
        this.transform.rotation = Quaternion.FromToRotation(this.transform.up, dir) * this.transform.rotation;
    }

    public string getName() {
        return entityName;
    }
}
