using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator
{
    private NoiseSettings settings;
    private PlanetEntity tree,mine;
    private static NoiseFilter noise = new NoiseFilter();
    private Planet planet;
    public ResourceGenerator(Planet planet,PlanetEntity mine, PlanetEntity tree,NoiseSettings noiseSettigns) {
        this.settings = new NoiseSettings();
        this.settings.baseRoughness = 3f;
        this.settings.numLayers = 2f;
        this.settings.strength = 2f;
        this.settings.persistance = 1.6f;
        this.settings.center = planet.transform.position;
        this.tree = tree;
        this.planet = planet;
        this.mine = mine;
    }

    public bool ShouldPlaceTree(Vector3 point)
    {
        this.settings.baseRoughness = 3f;
        this.settings.numLayers = 2f;
        this.settings.strength = 2f;
        this.settings.persistance = 1.6f;
        return noise.Evaluate(settings,point) <= 0.03f;
    }

    public bool ShouldPlaceMine(Vector3 point)
    {
        this.settings.baseRoughness = 2f;
        this.settings.numLayers = 2f;
        this.settings.strength = 2f;
        this.settings.persistance = 1.6f;
        return noise.Evaluate(settings, point) <= 0.03f;
    }

    public void PlaceTree(Vector3 point)
    {
       PlanetEntity obj = GameObject.Instantiate(tree,point,Quaternion.identity);
       obj.transform.SetParent(planet.transform);
       obj.RotateTowardsPlanet(planet);

    }

    public void PlaceMine(Vector3 point)
    {
        PlanetEntity obj = GameObject.Instantiate(mine, point, Quaternion.identity);
        obj.transform.SetParent(planet.transform);
        obj.RotateTowardsPlanet(planet);
    }
}
