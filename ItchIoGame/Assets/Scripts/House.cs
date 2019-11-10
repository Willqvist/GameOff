using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : PlanetEntity
{
    public int population;

    public override void PlaceOnPlanet(Planet planet, Vector3 position)
    {
        base.PlaceOnPlanet(planet, position);

        Planet.Current.Population += population;
    }
}
