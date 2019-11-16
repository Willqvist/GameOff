using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public float money;
    private Planet planet;

    public Planet Planet => planet;

    public void Awake()
    {
        Instance = this;
    }

    public void LoadPlanet(Planet planet)
    {
        this.planet = planet;
    }
}
