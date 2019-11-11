using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PrefabData : MonoBehaviour
{
    public static PrefabData Instance { get; private set; }

    [SerializeField] private PlanetEntity[] entities;

    private void Awake()
    {
        Instance = this;
    }

    public PlanetEntity GetEntity(EntityName name)
    {
        return this.entities.Where(x => x.entityData.entityName == name).FirstOrDefault();
    }
}
