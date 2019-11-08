using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHarvester : PlanetEntity
{
    private List<ResourceEntity> resourceEntities;

    private float tick;

    public void Start()
    {
        this.resourceEntities = new List<ResourceEntity>();
    }

    public override void Update()
    {
        base.Update();

        if(++tick >= 240)
        {
            foreach(var resource in this.resourceEntities)
            {
                Resource rs = resource.Harvest();
                if(rs != null)
                {
                    Player.Instance.AddResource(rs, 1);
                    Debug.Log("Harvesting...");
                }
            }

            tick = 0;
        }
    }

    public override void PlaceOnPlanet(Planet planet)
    {
        base.PlaceOnPlanet(planet);

        RaycastHit[] hits = Physics.SphereCastAll(new Ray(this.transform.position, this.transform.forward), 1);

        foreach(var hit in hits)
        {
            if(hit.transform.CompareTag("Resource"))
            {
                this.resourceEntities.Add(hit.transform.GetComponent<ResourceEntity>());
            }
        }
    }
}
