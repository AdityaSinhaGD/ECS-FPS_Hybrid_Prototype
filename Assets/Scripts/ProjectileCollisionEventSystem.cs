﻿using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Collections;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Burst;

[UpdateAfter(typeof(EndFramePhysicsSystem))]
public class ProjectileCollisionEventSystem : JobComponentSystem
{
    BuildPhysicsWorld m_BuildPhysicsWorldSystem;
    StepPhysicsWorld m_StepPhysicsWorldSystem;

    protected override void OnCreate()
    {
        base.OnCreate();
        m_BuildPhysicsWorldSystem = World.GetOrCreateSystem<BuildPhysicsWorld>();
        m_StepPhysicsWorldSystem = World.GetOrCreateSystem<StepPhysicsWorld>();
    }

    [BurstCompile(CompileSynchronously = true)]
    struct CollisionEventImpulseJob : ICollisionEventsJob
    {
        //[ReadOnly] public ComponentDataFromEntity<ProjectileData> projectileGroup;
        [ReadOnly] public ComponentDataFromEntity<LifetimeData> projectileGroup;
        public ComponentDataFromEntity<EnemyData> enemyGroup;

        public void Execute(CollisionEvent collisionEvent)
        {
            Entity entityA = collisionEvent.EntityA;
            Entity entityB = collisionEvent.EntityB;

            bool isTargetA = enemyGroup.Exists(entityA);
            bool isTargetB = enemyGroup.Exists(entityB);

            bool isProjectileA = projectileGroup.Exists(entityA);
            bool isProjectileB = projectileGroup.Exists(entityB);

            if (isProjectileA && isTargetB)
            {
                var isAliveComponent = enemyGroup[entityB];
                isAliveComponent.isAlive = false;
                enemyGroup[entityB] = isAliveComponent;
            }

            if (isProjectileB && isTargetA)
            {
                var isAliveComponent = enemyGroup[entityA];
                isAliveComponent.isAlive = false;
                enemyGroup[entityA] = isAliveComponent;
            }
        }
    }

    [BurstCompile(CompileSynchronously = true)]
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        JobHandle jobHandle = new CollisionEventImpulseJob
        {
            projectileGroup = GetComponentDataFromEntity<LifetimeData>(),
            enemyGroup = GetComponentDataFromEntity<EnemyData>()
        }.Schedule(m_StepPhysicsWorldSystem.Simulation, ref m_BuildPhysicsWorldSystem.PhysicsWorld, inputDeps);

        jobHandle.Complete();

        return jobHandle;

    }


}
