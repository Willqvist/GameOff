using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelObjectInfo : Panel
{
    private ObjectPanelComponent inst;
    public void Show(EntityData data)
    {
        if (inst == null)
        {
            inst = PanelInstance.CreateInstance<PanelInstanceObject>(PanelName.PANEL_OBJECT_INFO).GetComponent();
        }

        inst.title.SetText(data.name);
        inst.description.SetText(data.description);
        ShowEntityInfo(data);
        inst.closer.SetUIVisible(true);
    }
    private void ShowEntityInfo(EntityData data)
    {
        inst.info.text = "";
        if (data.electricity != 0)
        {
            string prefix = data.electricity > 0 ? "+" : "";
            string color = data.electricity < 0 ? "#FF0000" : "#00FF00";
            inst.info.text += $"<sprite=0> Electricity: <color={color}>{prefix}{data.electricity}\n</color>";
        }

        if (data.pollution != 0)
        {
            string prefix = data.pollution > 0 ? "+" : "";
            string color = data.pollution > 0 ? "#FF0000" : "#00FF00";
            inst.info.text += $"<sprite=1> Pollution: <color={color}>{prefix}{data.pollution}\n</color>";
        }

        if (data.activeWorkersRequirement > 0)
            inst.info.text += $"<sprite=3> Worker capacity: <color=#FFFFFF>{data.activeWorkersRequirement}\n</color>";

        if (data.populationIncrease != 0)
        {
            string prefix = data.populationIncrease > 0 ? "+" : "";
            string color = data.populationIncrease < 0 ? "#FF0000" : "#00FF00";
            inst.info.text += $"<sprite=2> Increases population by: <color={color}>{prefix}{data.populationIncrease}\n</color>";
        }
    }
}
