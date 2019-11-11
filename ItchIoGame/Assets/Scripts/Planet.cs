using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] public float gravity = -12;
    private List<PlanetEntity> entities;

    public void Start()
    {
        this.entities = new List<PlanetEntity>();
    }

    public void Add(PlanetEntity entity)
    {
        this.entities.Add(entity);
    }
}
