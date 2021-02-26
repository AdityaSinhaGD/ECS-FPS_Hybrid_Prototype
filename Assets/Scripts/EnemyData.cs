using Unity.Entities;
using Unity.Burst;

[BurstCompile]
[GenerateAuthoringComponent]
public struct EnemyData : IComponentData
{
    public bool isAlive;
}
