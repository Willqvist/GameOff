using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Create New Shop Item")]
public class ShopItem : ScriptableObject
{
    public GameObject prefab;
    public string name;
    public string buyButtonIdentifier;
    public string infoButtonIdentifier;
    public Resource electricityResource;
    public Resource moneyResource;
    public Resource populationResource;
}
