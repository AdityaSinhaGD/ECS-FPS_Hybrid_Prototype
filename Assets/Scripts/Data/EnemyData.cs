using Unity.Entities;
using Unity.Burst;

[GenerateAuthoringComponent]
public struct EnemyData : IComponentData
{
    public bool isAlive;
}
