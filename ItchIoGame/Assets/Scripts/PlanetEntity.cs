using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetEntity : MonoBehaviour {

    public Planet planet;
    public string entityName;
    public float colliderHeight = 2f;
    protected MaterialPropertyBlock prop;
    public void Start()
    {
        this.prop = new MaterialPropertyBlock();
        Debug.Log("initialisaxe: " + this.prop + " | " + this);
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

    public void SetColor(Color color)
    {
        Debug.Log(this.prop + " | " + color + " | " + this);
        this.prop.SetColor("_BaseColor", color);
        this.GetComponent<MeshRenderer>().SetPropertyBlock(this.prop);
    }

    public virtual void Update()
    {
        if(this.planet == null)
        {
            return;
        }

        //this.planet.Place(this.transform);
    }
    public virtual void OnPlace() { }
    public virtual void PlaceOnPlanet(Planet planet,Vector3 position)
    {
        this.planet = planet;
        this.transform.position = position;
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

    protected bool hasPlanet() {
        return planet != null;
    }

    public string getName() {
        return entityName;
    }
}
