using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    private Vector3 planetWorldPos;
    public float money;
    private Planet planet;

    public Planet Planet => planet;
    public Vector3 PlanetPos => planetWorldPos;
    public void Awake()
    {
        Instance = this;
    }

    public void LoadPlanet(Planet planet)
    {
        this.planet = planet;
        PlanetInformation.updateText();
    }
}
