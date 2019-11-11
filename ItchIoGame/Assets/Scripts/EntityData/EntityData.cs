using UnityEngine;

[CreateAssetMenu(fileName = "New EntityData", menuName = "EntityData")]
public class EntityData : ScriptableObject
{
    public EntityName entityName;
    public int cost;
    public int activeWorkersRequirement;
    public int populationIncrease;
    public float pollution;
    public float electricityCost;
    public float electricityGainPerSecond;
}

public enum EntityName
{
    House,
    Tree,
    Factory
}