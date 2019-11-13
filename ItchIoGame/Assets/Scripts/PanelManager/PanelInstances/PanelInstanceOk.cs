using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelInstanceOk : PanelInstance
{

    protected override PanelComponents GetComponentInObject()
    {
        return instance.GetComponent<OkPanelComponent>();
    }

    public OkPanelComponent GetComponent()
    {
        return (OkPanelComponent)this.instanciatedComponent;
    }
}
