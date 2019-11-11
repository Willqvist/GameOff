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

    public void ShowInfo(ShopItemData shopItemData)
    {
        if(shopItemData.entityData.electricityLosePerSecond > 0)
            this.context.text += $"Electricity cost: <color=#FF0000>-{shopItemData.entityData.electricityLosePerSecond}\n</color>";

        if(shopItemData.entityData.electricityGainPerSecond > 0)
            this.context.text += $"Electricity gain per second: <color=#00FF00>+{shopItemData.entityData.electricityGainPerSecond}\n</color>";

        if(shopItemData.entityData.pollution > 0)
            this.context.text += $"Pollution: <color=#FF0000>+{shopItemData.entityData.pollution}\n</color>";

        if (shopItemData.entityData.activeWorkersRequirement > 0)
            this.context.text += $"Worker capacity: <color=#FFFFFF>{shopItemData.entityData.activeWorkersRequirement}\n</color>";

        if (shopItemData.entityData.populationIncrease > 0)
            this.context.text += $"Increases population by: <color=#00FF00>+{shopItemData.entityData.populationIncrease}\n</color>";
    }
}
