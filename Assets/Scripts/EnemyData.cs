using Unity.Entities;

[GenerateAuthoringComponent]
public struct EnemyData : IComponentData
{
    public bool isAlive;
}
