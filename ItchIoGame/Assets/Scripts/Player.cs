using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private List<ResourceData> resources;

    public void Start()
    {
        Instance = this;
    }

    public void AddResource(Resource resource, int amount)
    {
        Debug.Log("Resource: " + resource);
        for(int i = 0; i < resources.Count; i++)
        {
            if (resources[i].resource.resourceType == resource.resourceType)
            {
                resources[i] = new ResourceData
                {
                    resource = resources[i].resource,
                    amount = resources[i].amount + amount
                };

                break;
            }
        }

        UiManager.Instance.UpdateResource(resource, amount);
    }
}
