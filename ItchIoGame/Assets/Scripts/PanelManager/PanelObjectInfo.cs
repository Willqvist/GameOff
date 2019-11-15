using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelObjectInfo : Panel
{
    private ObjectPanelComponent inst;
    public void Show(PlanetEntity entity)
    {
        if (inst == null)
        {
            inst = PanelInstance.CreateInstance<PanelInstanceObject>(PanelName.PANEL_OBJECT_INFO).GetComponent();
        }

        inst.title.SetText(entity.entityData.name);
        inst.description.SetText(entity.entityData.description);
        ShowEntityInfo(entity);
        inst.closer.SetUIVisible(true);
    }

    private void ShowEntityInfo(PlanetEntity entity)
    {
        inst.info.text = "";
        if (entity.entityData.electricity != 0)
        {
            string prefix = entity.entityData.electricity > 0 ? "+" : "";
            string color = entity.entityData.electricity < 0 ? "#FF0000" : "#00FF00";
            inst.info.text += $"<sprite=0> Electricity: <color={color}>{prefix}{entity.entityData.electricity} ({entity.ActiveStatus})</color>\n";
            
        }

        if (entity.entityData.pollution != 0)
        {
            string prefix = entity.entityData.pollution > 0 ? "+" : "";
            string color = entity.entityData.pollution > 0 ? "#FF0000" : "#00FF00";
            inst.info.text += $"<sprite=1> Pollution: <color={color}>{prefix}{entity.entityData.pollution}\n</color>";
        }

        if (entity.entityData.activeWorkersRequirement > 0)
        {
            inst.info.text += $"<sprite=3> Worker capacity: <color=#FFFFFF>{entity.ActiveWorkers} / {entity.entityData.activeWorkersRequirement}\n</color>";
        }

        if (entity.entityData.populationIncrease != 0)
        {
            string prefix = entity.entityData.populationIncrease > 0 ? "+" : "";
            string color = entity.entityData.populationIncrease < 0 ? "#FF0000" : "#00FF00";
            inst.info.text += $"<sprite=2> Increases population by: <color={color}>{prefix}{entity.entityData.populationIncrease}\n</color>";
        }
    }
}
