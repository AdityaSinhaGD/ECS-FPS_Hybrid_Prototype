using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;
using Unity.Physics;

public class LifetimeSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;

        Entities.WithoutBurst().WithStructuralChanges().ForEach((Entity entity, ref Translation translation, ref Rotation rotation, ref LifetimeData lifetimeData) =>
        {
            lifetimeData.lifeTimeRemaining -= deltaTime;
            if (lifetimeData.lifeTimeRemaining <= 0)
            {
                EntityManager.DestroyEntity(entity);
            }

        }).Run();

        return inputDeps;
    }


}