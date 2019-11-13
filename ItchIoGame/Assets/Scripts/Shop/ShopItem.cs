using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public ButtonController buyButton;
    public TextMeshProUGUI title;
    public TextMeshProUGUI price;
    public TextMeshProUGUI buybuttonText;
    public ShopItemInfo info;
    private bool isBuying = false;
    ObjectPlacerListener listener;
    public void Load(ShopItemData shopItemData)
    {
        this.title.text = shopItemData.entityData.entityName.ToDescription();
        this.price.text = "$" + shopItemData.entityData.cost;
        this.info.ShowInfo(shopItemData);
        this.buyButton.RegisterOnClickEvent(() =>
        {
            if (isBuying && listener != null)
            {
                listener.SignalOnCancel();
                return;
            }
            if (ObjectPlacerListener.IsWorking()) return;
            Debug.Log("buying");
            listener = ObjectPlacerListener.create();
            listener.OnCancelListener(OnCancel);
            listener.OnPlaceListener(OnPlace);
            Player.Instance.Planet.GetComponent<PlanetObjectPlacer>().PlaceObject(listener,shopItemData.entityData.entityName);
            buybuttonText.SetText("X Buy");
            isBuying = true;

        });
    }

    private void OnCancel()
    {
        isBuying = false;
        //PanelManager.Get<PanelOk>().Show("Cancel Buy", "You sure!?", (b) => { Debug.Log("click on: " + b); });
        buybuttonText.SetText("$ Buy");
        ObjectPlacerListener.clear();
        listener = null;
    }

    private void OnPlace()
    {
        OnCancel();
    }
}
