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
        if (shopItemData.entityData.electricity != 0)
        {
            string color = shopItemData.entityData.electricity < 0 ? "#FF0000" : "#00FF00";
            this.context.text += $"<sprite=0> Electricity: <color={color}>{shopItemData.entityData.electricity}\n</color>";
        }

        if (shopItemData.entityData.pollution != 0)
        {
            string color = shopItemData.entityData.pollution > 0 ? "#FF0000" : "#00FF00";
            this.context.text += $"<sprite=1> Pollution: <color={color}>{shopItemData.entityData.pollution}\n</color>";
        }

        if (shopItemData.entityData.activeWorkersRequirement > 0)
            this.context.text += $"<sprite=3> Worker capacity: <color=#FFFFFF>{shopItemData.entityData.activeWorkersRequirement}\n</color>";

        if (shopItemData.entityData.populationIncrease != 0)
        {
            string color = shopItemData.entityData.populationIncrease < 0 ? "#FF0000" : "#00FF00";
            this.context.text += $"<sprite=2> Increases population by: <color={color}>{shopItemData.entityData.populationIncrease}\n</color>";

        }
    }
}
