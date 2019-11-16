using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlanetEntityInfo : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void Start()
    {
        this.text = this.GetComponent<TextMeshProUGUI>();
    }

    public void ShowInfo(EntityData entityData)
    {
        HandleStyling(entityData, this.text);
    }

    public static void HandleStyling(EntityData entityData, TextMeshProUGUI context)
    {
        if (entityData.electricity != 0)
        {
            string prefix = entityData.electricity > 0 ? "+" : "";
            string color = entityData.electricity < 0 ? "#FF0000" : "#00FF00";
            context.text += $"<sprite=0> Electricity: <color={color}>{prefix}{entityData.electricity}\n</color>";
        }

        if (entityData.pollution != 0)
        {
            string prefix = entityData.pollution > 0 ? "+" : "";
            string color = entityData.pollution > 0 ? "#FF0000" : "#00FF00";
            context.text += $"<sprite=1> Pollution: <color={color}>{prefix}{entityData.pollution}\n</color>";
        }

        if (entityData.activeWorkersRequirement > 0)
            context.text += $"<sprite=3> Worker capacity: <color=#FFFFFF>{entityData.activeWorkersRequirement}\n</color>";

        if (entityData.populationIncrease != 0)
        {
            string prefix = entityData.populationIncrease > 0 ? "+" : "";
            string color = entityData.populationIncrease < 0 ? "#FF0000" : "#00FF00";
            context.text += $"<sprite=2> Increases population by: <color={color}>{prefix}{entityData.populationIncrease}\n</color>";
        }
    }
}
