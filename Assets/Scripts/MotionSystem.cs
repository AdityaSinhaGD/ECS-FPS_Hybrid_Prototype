using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;
using Unity.Physics;
using Unity.Burst;

[BurstCompile(CompileSynchronously = true)]
public class MotionSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;

        JobHandle jobhHandle = Entities.ForEach((ref Translation position, ref Rotation rotation, ref PhysicsVelocity physicsVelocity, in MotionData motion) =>
        {
            float sinMovementValue = math.sin((deltaTime + position.Value.x) * 0.5f) * motion.motionSpeed;
            float cosMovementValue = math.cos((deltaTime + position.Value.y) * 0.5f) * motion.motionSpeed;

            float3 direction = new float3(sinMovementValue, cosMovementValue, sinMovementValue);

            //position.Value = new float3(position.Value.x + cosMovementValue, position.Value.y + sinMovementValue, position.Value.z + sinMovementValue);

            physicsVelocity.Linear += direction;


        }).Schedule(inputDeps);

        jobhHandle.Complete();

        return jobhHandle;
    }
}
