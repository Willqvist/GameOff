using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public List<ShopItem> shopItems;

    public ShopItemInstance shopItemPrefab;

    void Start()
    {
        int index = 0;
        foreach(ShopItem s in this.shopItems)
        {
            ShopItemInstance i = Instantiate(this.shopItemPrefab);
            i.transform.SetParent(this.transform);
            i.transform.localScale = Vector3.one;
            i.gameObject.SetActive(true);
            i.GetComponent<RectTransform>().position = new Vector3(i.GetComponent<RectTransform>().position.x, i.GetComponent<RectTransform>().position.y * (4f * index++), i.GetComponent<RectTransform>().position.z);
            i.Load(s);
        }
    }
}
