using UnityEngine;

[CreateAssetMenu(fileName = "New EntityData", menuName = "EntityData")]
public class EntityData : ScriptableObject
{
    public EntityName entityName;
    public int cost;
    public int activeWorkersRequirement;
    public int populationIncrease;
    public float pollution;
    public float electricityLosePerSecond;
    public float electricityGainPerSecond;
}

public enum EntityName
{
    [Description("House")] House,
    [Description("Tree")] Tree,
    [Description("Factory")] Factory,
    [Description("Human")] Human
}