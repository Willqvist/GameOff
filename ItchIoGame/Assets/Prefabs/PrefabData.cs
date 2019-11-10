using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabData : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public PlanetEntity[] entities;
    private static Dictionary<string, PlanetEntity> planetEntities = new Dictionary<string, PlanetEntity>();

    private void Start()
    {
        foreach (PlanetEntity entity in entities) {
            planetEntities[entity.getName()] = entity;
        }
    }

    public static PlanetEntity GetEntity(string name)
    {
        return planetEntities[name];
    }
}
