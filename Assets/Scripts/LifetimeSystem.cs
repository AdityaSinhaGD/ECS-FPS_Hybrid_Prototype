using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;
using Unity.Physics;
using Unity.Burst;

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

        Entities.WithoutBurst().WithStructuralChanges().ForEach((Entity entity, ref Translation translation, ref Rotation rotation, ref EnemyData enemyData) =>
        {
            
            if (!enemyData.isAlive)
            {
                for(int i = 0; i < 100; i++)
                {
                    float3 offset = (float3)UnityEngine.Random.insideUnitSphere * 2.0f;

                    var spawn = EntitySpawner.entityManager.Instantiate(EntitySpawner.spawnedAfterEnemyDeathEntity);

                    float3 randomDirection = new float3(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1));

                    EntitySpawner.entityManager.SetComponentData(spawn, new Translation
                    {
                        Value = translation.Value + offset
                    });

                    EntitySpawner.entityManager.SetComponentData(spawn, new PhysicsVelocity
                    {
                        Linear = randomDirection * 2
                    });
                }


                EntityManager.DestroyEntity(entity);
            }

        }).Run();

        return inputDeps;
    }


}