using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; }

    [SerializeField] private Text treeResourceAmount;
    [SerializeField] private Text ironResourceAmount;

    void Start()
    {
        Instance = this;
    }

    public void UpdateResource(Resource resource, int amount)
    {
        if(resource.resourceType == ResourceType.WOOD)
        {
            this.treeResourceAmount.text = (int.Parse(this.treeResourceAmount.text) + amount).ToString();
        }

        if (resource.resourceType == ResourceType.IRON)
        {
            this.ironResourceAmount.text = (int.Parse(this.ironResourceAmount.text) + amount).ToString();
        }
    }
}
