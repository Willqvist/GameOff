using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceEntity : MonoBehaviour
{
    [SerializeField] private Resource resource;
    [SerializeField] private int value;

    public Resource Harvest()
    {
        if(this.value > 0)
        {
            this.value--;
            return this.resource;
        }

        return null;
    }
}
