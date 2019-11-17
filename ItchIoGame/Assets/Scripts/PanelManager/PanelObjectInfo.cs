using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelObjectInfo : Panel
{
    private ObjectPanelComponent inst;

    public void Hide()
    {
        if(inst != null)
            inst.gameObject.SetActive(false);
    }

    public void Show(PlanetEntity entity)
    {
        if (inst == null)
        {
            inst = PanelInstance.CreateInstance<PanelInstanceObject>(PanelName.PANEL_OBJECT_INFO).GetComponent();
        }

        inst.title.SetText(entity.entityData.name);
        inst.description.SetText(entity.entityData.description);

        if(entity.entityData.canSell)
        {
            this.inst.sellAmount.gameObject.SetActive(true);
            this.inst.sellButton.gameObject.SetActive(true);

            inst.sellAmount.SetText("$ " + (entity.entityData.cost * ConstMultiplier.MONEY).ToString());
            inst.sellButton.ClearEvents();
            inst.sellButton.RegisterOnClickEvent(() =>
            {
                if (entity.gameObject == null)
                {
                    return;
                }

                Player.Instance.money += entity.entityData.cost * ConstMultiplier.MONEY;
                Object.Destroy(entity.gameObject);
                this.inst.gameObject.SetActive(false);
            });
        }
        else
        {
            this.inst.sellAmount.gameObject.SetActive(false);
            this.inst.sellButton.gameObject.SetActive(false);
        }

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

        if(entity.entityData.happiness != 0)
        {
            string prefix = entity.entityData.happiness > 0 ? "+" : "";
            string color = entity.entityData.happiness < 0 ? "#FF0000" : "#00FF00";
            inst.info.text += $"<sprite=2> Happiness: <color={color}>{prefix}{entity.entityData.happiness}\n</color>";
        }
    }
}
