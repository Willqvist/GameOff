using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlanetType", menuName = "PlanetType")]
public class PlanetType : ScriptableObject
{
    public PlanetName planetName;
    public Texture2D planetTexture;
    public Vector2 radiusScale;
    public Vector2 baseRoughnessScale;
    public Vector2 numLayersRange;
    public Vector2 strengthRange;
    public Vector2 persistanceRange;
}

public enum PlanetName
{
    [Description("Normal")] Normal,
    [Description("Lava")] Lava
}
