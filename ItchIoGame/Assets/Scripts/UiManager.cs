using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; }

    [SerializeField] private Text treeResourceAmount;
    [SerializeField] private Text ironResourceAmount;
    [SerializeField] private TextMeshProUGUI electricityPerSecond;
    [SerializeField] private TextMeshProUGUI planetHealth;
    [SerializeField] private TextMeshProUGUI planetPopulation;

    void Start()
    {
        Instance = this;
    }

    public void IncreaseResource(Resource resource, int amount)
    {
        UpdateResource(resource, amount, 1);
    }

    public void DecreaseResource(Resource resource, int amount)
    {
        UpdateResource(resource, amount, -1);
    }

    public void UpdateResource(Resource resource, int amount, int shift)
    {
        if (resource.resourceType == ResourceType.WOOD)
        {
            this.treeResourceAmount.text = (int.Parse(this.treeResourceAmount.text) + (amount * shift)).ToString();
        }

        if (resource.resourceType == ResourceType.IRON)
        {
            this.ironResourceAmount.text = (int.Parse(this.ironResourceAmount.text) + (amount * shift)).ToString();
        }

        if (resource.resourceType == ResourceType.ELECTRICITY)
        {
            this.electricityPerSecond.text = (int.Parse(this.electricityPerSecond.text) + (amount * shift)).ToString();
        }
    }

    public void UpdatePlanetHealth(float health)
    {
        this.planetHealth.text = ((int)health).ToString();
        if(health < 70)
        {
            this.planetHealth.color = Color.yellow;
        }
        if(health < 40)
        {
            this.planetHealth.color = Color.red;
        }
    }

    public void UpdatePopulation(int population)
    {
        this.planetPopulation.text = Planet.Current.ActiveWorkers + " / " + population.ToString();
    }
}
