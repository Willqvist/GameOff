using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOk : Panel
{
    private OkPanelComponent component;
    private Action<bool> clb;
    public void Show(string title, string msg, Action<bool> clb)
    {
        this.clb = clb;
        component = PanelInstance.GetInstance<PanelInstanceOk>(PanelName.PANEL_OK).GetComponent();
        component.closer.SetUIVisible(true);
        Debug.Log("here");
        component.title.SetText(title);
        component.text.SetText(msg);
        component.okController.RegisterOnClickEvent(() =>
        {
            Debug.Log("Button click");
            component.closer.SetUIVisible(false);
            clb(true);
        });

        component.cancelController.RegisterOnClickEvent(Cancel);
        component.closeController.RegisterOnClickEvent(Cancel);
    }

    private void Cancel()
    {
        component.closer.SetUIVisible(false);
        this.clb(false);
    }
}
