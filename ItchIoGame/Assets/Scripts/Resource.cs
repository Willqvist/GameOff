using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Resource")]
public class Resource : ScriptableObject
{
    public ResourceType resourceType;
}

public enum ResourceType
{
    WOOD,
    IRON
}