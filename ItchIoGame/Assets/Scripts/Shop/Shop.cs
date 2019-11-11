using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopItemPrefab;
    public GameObject contentPrefab;
    public List<ShopItemData> shopItems;

    public void Start()
    {
        foreach(ShopItemData data in this.shopItems)
        {
            GameObject inst = Instantiate(this.shopItemPrefab);
            inst.transform.SetParent(this.contentPrefab.transform);
            inst.transform.localScale = Vector3.one;
            inst.SetActive(true);
            inst.GetComponent<ShopItem>().Load(data);
        }
    }
}
