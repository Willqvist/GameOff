using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetEntity : MonoBehaviour 
{
    public EntityData entityData;
    public string entityName;
    public float colliderHeight = 2f;
    public float sphereRadius = 1;
    private int collidingEntities = 0;
    protected Planet planet;

    public virtual void Start()
    {
        this.PlaceOnPlanet(this.planet, this.transform.position);
    }

    public virtual void Update()
    {

    }

    public virtual void OnPlace() { }
    public virtual void PlaceOnPlanet(Planet planet,Vector3 position)
    {
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
    public bool IsCollidingEntity() {
        return collidingEntities > 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.layer);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Planet Object")) {
            collidingEntities++;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Planet Object"))
        {
            collidingEntities--;
        }
    }
}
