using UnityEngine;

[CreateAssetMenu(fileName = "New EntityData", menuName = "EntityData")]
public class EntityData : ScriptableObject
{
    public EntityName entityName;
    public int cost;
    public int activeWorkersRequirement;
    public int populationIncrease;
    public float pollution;
    public float electricity;
    public float happiness;
    public string description;
    public bool canSell;
    public ScriptableObject externalData;
}

public enum EntityName
{
    [Description("House")] House,
    [Description("Tree")] Tree,
    [Description("Factory")] Factory,
    [Description("Human")] Human,
    [Description("Vespene")] Vespene,
    [Description("Mine")] Mine,
    [Description("Flower")] Flower,
    [Description("Teleporter")] Teleporter
}