using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator
{
    private ShapeSettings settings;
    private NoiseSettings noiseSettings;
    public MinMax elevationMinMax;
    private NoiseFilter noiseFilter;
    private Texture2D texutre;
    public ShapeGenerator(NoiseSettings noiseSettings,ShapeSettings settings) {
        this.settings = settings;
        noiseFilter = new NoiseFilter();
        elevationMinMax = new MinMax();
        this.noiseSettings = noiseSettings;
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere) {
        float elevation = settings.planetRadius * (1 + noiseFilter.Evaluate(noiseSettings,pointOnUnitSphere));
        elevationMinMax.addValue(elevation);
        return pointOnUnitSphere * elevation;
    }
}
