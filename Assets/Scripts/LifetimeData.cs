using Unity.Entities;
using Unity.Burst;

[BurstCompile(CompileSynchronously = true)]
[GenerateAuthoringComponent]
public struct LifetimeData : IComponentData
{
    public float lifeTimeRemaining;
}
