using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PanelInstanceObject : PanelInstance
{

    protected override PanelComponents GetComponentInObject()
    {
        return instance.GetComponent<ObjectPanelComponent>();
    }

    public ObjectPanelComponent GetComponent()
    {
        return (ObjectPanelComponent)this.instanciatedComponent;
    }

}
