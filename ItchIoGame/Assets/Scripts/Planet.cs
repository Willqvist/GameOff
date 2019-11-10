using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private float radius;

    public float electricity;
    private int population;
    public int Population
    {
        get { return population;  }
        set { population = value; UiManager.Instance.UpdatePopulation(population); }
    }

    private int activeWorkers;
    public int ActiveWorkers
    {
        get { return activeWorkers; }
        set { activeWorkers = value; UiManager.Instance.UpdatePopulation(population); }
    }

    public static Planet Current { get; private set; }

    public float health = 100;

    private void Awake()
    {
        Current = this;
    }

    public void Place(Transform body)
    {
        Vector3 dir = (body.position - this.transform.position).normalized;
        body.rotation = Quaternion.FromToRotation(body.up, dir) * body.rotation;
        body.position = (dir * radius) + this.transform.position;
    }

    public void Pollute(float pollutionPerSecond)
    {
        this.health -= pollutionPerSecond;
        UiManager.Instance.UpdatePlanetHealth(this.health);
    }
}
