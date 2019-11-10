using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItemInstance : MonoBehaviour
{
    public TextMeshProUGUI title;
    public BuyButton buyButton;
    public ButtonController infoButton;
    public TextMeshProUGUI electricityPerSecondText;
    public TextMeshProUGUI population;

    private ShopItem shopItem;

    public void Load(ShopItem shopItem)
    {
        this.shopItem = shopItem;

        this.title.text = shopItem.name;
        this.buyButton.SetIdentifier(shopItem.buyButtonIdentifier);
        this.buyButton.SetShopItemContext(this);
        this.infoButton.SetIdentifier(shopItem.infoButtonIdentifier);
        this.electricityPerSecondText.text = (shopItem.electricityResource.value > 0 ? "+" : "") + shopItem.electricityResource.value + "el/s";
        this.population.text = (shopItem.populationResource.value > 0 ? "+" : "") + shopItem.populationResource.value;

        if (shopItem.populationResource.value < 0)
        {
            this.population.color = Color.red;
        }
        else if(shopItem.populationResource.value > 0)
        {
            this.population.color = Color.green;
        }
        else
        {
            this.population.color = Color.gray;
        }

        if(shopItem.electricityResource.value > 0)
        {
            this.electricityPerSecondText.color = Color.green;
        }
        else if (shopItem.electricityResource.value < 0)
        {
            this.electricityPerSecondText.color = Color.red;
        }
        else
        {
            this.electricityPerSecondText.color = Color.gray;
        }
    }

    public bool CanBuy()
    {
        bool canBuy = false;

        if(this.HasLoaded())
        {
            if(Planet.Current.electricity >= -this.shopItem.electricityResource.value)
            {
                canBuy = true;
            }
            else
            {
                ErrorLogController.Instance.LogError("[SHOP] - Planet does not contain enough electricity to afford this item");
                return false;
            }

            if (Planet.Current.ActiveWorkers < -this.shopItem.populationResource.value && Planet.Current.ActiveWorkers + (-this.shopItem.populationResource.value) < Planet.Current.Population)
            {
                canBuy = true;
            }
            else
            {
                ErrorLogController.Instance.LogError("[SHOP] - Planet does not have big enough population to use this item");
                return false;
            }
        }

        return canBuy;
    }

    private bool HasLoaded()
    {
        return this.shopItem != null;
    }
}
