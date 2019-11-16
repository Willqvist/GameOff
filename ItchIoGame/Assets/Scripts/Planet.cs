using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] public float gravity = -12;
    private List<PlanetEntity> entities;
    private Stack<PlanetEntity> lowElectricityQueue;
    private Stack<PlanetEntity> lowPopulationQueue;

    public int activeWorkers;
    public int population;
    public float pollution;
    public float electricity;
    public float happiness;

    public void Start()
    {
        this.entities = new List<PlanetEntity>();
        this.lowElectricityQueue = new Stack<PlanetEntity>();
        this.lowPopulationQueue = new Stack<PlanetEntity>();
    }

    public void Update()
    {
        if(this.electricity > 0)
        {
            while(this.lowElectricityQueue.Count > 0)
            {
                PlanetEntity e = this.lowElectricityQueue.Pop();
                e.StartRunning();
            }
        }

        if(this.activeWorkers < this.population)
        {
            while (this.lowPopulationQueue.Count > 0 && this.activeWorkers < this.population)
            {
                PlanetEntity e = this.lowPopulationQueue.Pop();
                e.AddWorkers(this.population - this.activeWorkers >= e.entityData.activeWorkersRequirement ? e.entityData.activeWorkersRequirement : this.population - this.activeWorkers);
                this.activeWorkers += e.ActiveWorkers;
                e.StartRunning();
            }
        }
    }

    public void Add(PlanetEntity entity)
    {
        this.entities.Add(entity);
    }

    public void Pollute(float pollution)
    {
        this.pollution += pollution;
    }

    public void AddElectricityPerSecond(float electricity)
    {
        this.electricity += electricity;
    }

    public void QueueEntityForLowElectricityActivation(PlanetEntity entity)
    {
        this.lowElectricityQueue.Push(entity);
    }

    public void QueueEntityForLowPopulationActivation(PlanetEntity entity)
    {
        this.lowPopulationQueue.Push(entity);
    }
}
