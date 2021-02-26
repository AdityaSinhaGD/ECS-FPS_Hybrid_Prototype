using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;
using Unity.Physics;

public class ProjectileMotionSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;

        JobHandle jobHandle = Entities.ForEach((ref Translation translation, ref Rotation rotation, ref PhysicsVelocity physicsVelocity, in ProjectileData projectileData) =>
        {
            physicsVelocity.Angular = float3.zero;
            physicsVelocity.Linear += projectileData.projectileSpeed * math.forward(rotation.Value) * deltaTime;

        }).Schedule(inputDeps);

        jobHandle.Complete();

        return jobHandle;
    }

   
}
