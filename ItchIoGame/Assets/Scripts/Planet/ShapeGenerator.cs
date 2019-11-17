using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator
{
    private float radius;
    private NoiseSettings noiseSettings;
    public MinMax elevationMinMax;
    private NoiseFilter noiseFilter;
    private Texture2D texutre;
    public ShapeGenerator(NoiseSettings noiseSettings,float radius) {
        this.radius = radius;
        noiseFilter = new NoiseFilter();
        elevationMinMax = new MinMax();
        this.noiseSettings = noiseSettings;
    }

    public float GetRadius()
    {
        return radius;
    }
    public float getNoise(Vector3 pointOnUnitSphere) {
        return noiseFilter.Evaluate(noiseSettings, pointOnUnitSphere);
    }
    public Vector3 CalculatePointOnPlanet(float noise, Vector3 pointOnUnitSphere) {
        float elevation = radius * (1 + noise);
        elevationMinMax.addValue(elevation);
        return pointOnUnitSphere * elevation;
    }
}
