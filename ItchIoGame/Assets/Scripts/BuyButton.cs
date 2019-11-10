using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class BuyButton : ButtonController
{
    private ShopItemInstance shopItem;

    public void SetShopItemContext(ShopItemInstance shopItem)
    {
        this.shopItem = shopItem;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (this.shopItem.CanBuy())
        {
            base.OnPointerUp(eventData);
        }
        else
        {
            if (this.onClick == null)
            {
                Debug.Log("On click is null in ButtonController::OnPointerUp");
                return;
            }

            animator.SetBool("clicking", false);
        }
    }
}
