using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PlanetEntity : MonoBehaviour 
{
    public EntityData entityData;
    public float colliderHeight = 2f;
    public float sphereRadius = 1;
    private int collidingEntities = 0;
    protected Planet planet;
    private int activeWorkers;
    private bool hasPlaced;
    public bool isRunning = true;

    public bool HasPlaced => hasPlaced;
    public int ActiveWorkers => activeWorkers;
    public string ActiveStatus => isRunning ? "<color=#00FF00>Active</color>" : "<color=#FF0000>Inactive</color>";

    public virtual void Start()
    {
        //this.PlaceOnPlanet(this.planet, this.transform.position);
    }

    public virtual void Update()
    {

    }

    public virtual void OnPlace() { }
    public virtual void PlaceOnPlanet(Planet planet,Vector3 position)
    {
        this.planet = planet;
        this.transform.position = position;
        this.activeWorkers = (planet.population - planet.activeWorkers) >= this.entityData.activeWorkersRequirement ? this.entityData.activeWorkersRequirement : (planet.population - planet.activeWorkers);

        planet.activeWorkers += this.activeWorkers;

        if (this.entityData.pollution != 0)
        {
            this.StartCoroutine(Pollute(this.entityData.pollution));
        }

        if (this.entityData.electricity != 0)
        {
            planet.AddElectricityPerSecond(this.entityData.electricity);
        }

        if(this.entityData.populationIncrease > 0)
        {
            planet.population += this.entityData.populationIncrease;
        }

        this.hasPlaced = true;

        if(this.activeWorkers < this.entityData.activeWorkersRequirement)
        {
            isRunning = false;
            planet.QueueEntityForLowPopulationActivation(this);
        }

        if(Mathf.Abs(this.entityData.electricity) > planet.electricity)
        {
            isRunning = false;
            planet.QueueEntityForLowElectricityActivation(this);
        }

        planet.happiness += this.entityData.happiness;
    }

    public void StartRunning()
    {
        this.isRunning = true;
    }

    public float getColliderHeight()
    {
        return this.colliderHeight;
    }

    public void AddWorkers(int workers)
    {
        this.activeWorkers += workers;
    }

    public void RotateTowardsPlanet(Planet planet)
    {
        Vector3 dir = (this.transform.position - planet.transform.position).normalized;
        this.transform.rotation = Quaternion.FromToRotation(this.transform.up, dir) * this.transform.rotation;
    }

    protected bool hasPlanet() {
        return planet != null;
    }

    public bool IsCollidingEntity() {
        return collidingEntities > 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
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

    private IEnumerator Pollute(float pollution)
    {
        yield return new WaitForSeconds(1);

        if((pollution > 0 || Player.Instance.Planet.pollution > 0) && this.isRunning && this.activeWorkers > 0)
        {
            this.planet.Pollute(pollution);
        }
        
        this.StartCoroutine(this.Pollute(this.entityData.pollution));
    }
}
