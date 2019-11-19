using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] public float radius;
    [SerializeField] public float gravity = -12;
    private List<PlanetEntity> entities;
    private Stack<PlanetEntity> lowElectricityQueue;
    private Stack<PlanetEntity> lowPopulationQueue;
    public PlanetCamera planetCamera;
    private static int ids = 0;
    private int id = -1;
    private PlanetGenerator generator;
    private PlanetType type;
    public int activeWorkers;
    public int population;
    public float pollution;
    public float electricity;
    public float happiness;

    public int Id => ids;

    public PlanetType PType()
    {
        return type;
    }

    public void Start()
    {
        if (id != -1) return;
        this.entities = new List<PlanetEntity>();
        this.lowElectricityQueue = new Stack<PlanetEntity>();
        this.lowPopulationQueue = new Stack<PlanetEntity>();
        this.generator = this.GetComponent<PlanetGenerator>();
        id = ids++;
    }

    public void Initalize(Vector3 position,PlanetType type)
    {
        if (id == -1)
        {
            this.entities = new List<PlanetEntity>();
            this.lowElectricityQueue = new Stack<PlanetEntity>();
            this.lowPopulationQueue = new Stack<PlanetEntity>();
            this.generator = this.GetComponent<PlanetGenerator>();
            id = ids++;
        }
        this.type = type;
        NoiseSettings settings = GenerateSettings(type,position);
        /*
        settings.baseRoughness = 0.6f;
        settings.center = position;
        */
        generator.InitPlanet(type,settings, radius);
        this.gameObject.transform.position = position;

    }

    private NoiseSettings GenerateSettings(PlanetType type, Vector3 center)
    {
        NoiseSettings settings = new NoiseSettings();
        settings.baseRoughness = Random.Range(type.baseRoughnessScale.x, type.baseRoughnessScale.y);
        settings.numLayers = Random.Range(type.numLayersRange.x, type.numLayersRange.y);
        settings.strength = Random.Range(type.strengthRange.x, type.strengthRange.y);
        settings.persistance = Random.Range(type.persistanceRange.x, type.persistanceRange.y);
        settings.center = center;
        return settings;
    }

    public void OnEnter()
    {

    }

    public void OnLeave()
    {
        
    }

    public void Update()
    {
        if (!this.generator.Loaded()) return;
        if(this.electricity > 0)
        {
            while(this.lowElectricityQueue.Count > 0)
            {
                PlanetEntity e = this.lowElectricityQueue.Pop();

                if(!this.lowPopulationQueue.Contains(e))
                {
                    e.StartRunning();
                }
            }
        }

        if(this.activeWorkers < this.population)
        {
            while (this.lowPopulationQueue.Count > 0 && this.activeWorkers < this.population)
            {
                PlanetEntity e = this.lowPopulationQueue.Pop();
                e.AddWorkers(this.population - this.activeWorkers >= e.entityData.activeWorkersRequirement ? e.entityData.activeWorkersRequirement : this.population - this.activeWorkers);
                this.activeWorkers += e.ActiveWorkers;

                if (!this.lowElectricityQueue.Contains(e))
                {
                    e.StartRunning();
                }
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

    private void OnMouseDown()
    {
        Debug.Log(this);
        planetCamera.PivotTranslate(this.transform.position);
        Player.Instance.LoadPlanet(this);
    }
}
