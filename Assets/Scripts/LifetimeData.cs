using Unity.Entities;
using Unity.Burst;

[GenerateAuthoringComponent]
public struct LifetimeData : IComponentData
{
    public float lifeTimeRemaining;
}
