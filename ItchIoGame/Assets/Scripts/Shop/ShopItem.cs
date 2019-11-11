using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public ButtonController buyButton;
    public TextMeshProUGUI title;
    public TextMeshProUGUI price;
    public ShopItemInfo info;

    public void Load(ShopItemData shopItemData)
    {
        this.title.text = shopItemData.entityData.entityName.ToDescription();
        this.price.text = "$" + shopItemData.entityData.cost;
        this.info.ShowInfo(shopItemData);
        this.buyButton.RegisterOnClickEvent(() =>
        {
            Player.Instance.Planet.GetComponent<PlanetObjectPlacer>().PlaceObject(shopItemData.entityData.entityName);
        });
    }
}
