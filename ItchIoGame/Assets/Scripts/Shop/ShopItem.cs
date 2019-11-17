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
    private Quaternion telBuyRot;
    ObjectPlacerListener listener;

    public void Load(ShopItemData shopItemData)
    {
        this.title.text = shopItemData.entityData.entityName.ToDescription();
        this.price.text = "$" + shopItemData.entityData.cost;
        this.info.ShowInfo(shopItemData);
        this.buyButton.RegisterOnClickEvent(() =>
        {
            UI.Instance.ShopItem = shopItemData;
            if (Player.Instance.money < shopItemData.entityData.cost)
            {
                //Show some stupid message here
                return;
            }

            if (isBuying && listener != null)
            {
                listener.SignalOnCancel();
                return;
            }
            buybuttonText.SetText("X Buy");
            isBuying = true;

            if (shopItemData.entityData.name.Equals("Teleporter"))
            {
                telBuyRot = UI.Instance.planetCamera.transform.rotation;
                listener = ObjectPlacerListener.create();
                listener.OnCancelListener(OnCancelTeleport);
                listener.OnPlaceListener(OnPlace);
                UI.Instance.planetCamera.SetState(CameraState.TELEPORT);
                UI.Instance.planetCamera.PivotTranslate(Player.Instance.Planet.transform.position + new Vector3(0, 300, 0));
                UI.Instance.planetCamera.PivotTranslateRotation(Quaternion.Euler(90, 0, 0));
                UI.Instance.planetCamera.DisableRotation(true);
                UI.Instance.ShowArea(Player.Instance.Planet.transform.position,((TeleporterData)shopItemData.entityData.externalData).radius*2);
            }
            else
            {
                if (ObjectPlacerListener.IsWorking()) return;
                Debug.Log("buying");
                listener = ObjectPlacerListener.create();
                listener.OnCancelListener(OnCancel);
                listener.OnPlaceListener(OnPlace);
                Player.Instance.Planet.GetComponent<PlanetObjectPlacer>().PlaceObject(listener, shopItemData.entityData.entityName, () =>
                 {
                     Player.Instance.money -= shopItemData.entityData.cost;
                 });

            }
        });
    }
    private void OnCancelTeleport()
    {
        OnCancel();
        UI.Instance.HideArea();
        UI.Instance.planetCamera.PivotTranslate(Player.Instance.Planet.transform.position);
        UI.Instance.planetCamera.PivotTranslateRotation(telBuyRot);
        UI.Instance.planetCamera.DisableRotation(false);
        UI.Instance.planetCamera.SetState(CameraState.PLANET);
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
