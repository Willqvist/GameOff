using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetLoader : MonoBehaviour
{
    [SerializeField] private List<Planet> planets;

    private void Start()
    {
        Player.Instance.LoadPlanet(planets[0]);
    }
}
