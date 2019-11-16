using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ShopItemInfo : MonoBehaviour
{
    private TextMeshProUGUI context;

    private void Awake()
    {
        this.context = this.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
    }

    public void ShowInfo(ShopItemData shopItemData)
    {
        PlanetEntityInfo.HandleStyling(shopItemData.entityData, this.context);
    }

}
