using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHarvester : PlanetEntity
{
    private List<ResourceEntity> resourceEntities;

    private float tick;

    public void Start()
    {
        base.Start();
        this.resourceEntities = new List<ResourceEntity>();
        /*
        BoxCollider box = this.GetComponent<BoxCollider>();
        this.colliderHeight = box.size.y;
        Debug.Log("height: " + this.colliderHeight);
        */
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

    public override void PlaceOnPlanet(Planet planet,Vector3 point)
    {
        base.PlaceOnPlanet(planet,point);

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
