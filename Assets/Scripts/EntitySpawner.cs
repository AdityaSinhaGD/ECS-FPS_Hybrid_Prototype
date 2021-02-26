using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class EntitySpawner : MonoBehaviour
{
    public static EntityManager entityManager;

    public GameObject enemyPrefab;
    public GameObject neutralPrefab;
    public GameObject projectilePrefab;
    public GameObject spawnedAfterEnemyDeathPrefab;

    int numberOfEnemyPrefabs = 5000;
    int numberOfNeutralPrefabs = 5000;
    int numberOfProjectilesPerFire = 15;

    private Transform projectileSpawnTransform;

    BlobAssetStore assetStore;

    World world;

    Entity projectileEntity;
    public static Entity spawnedAfterEnemyDeathEntity;

    private void Awake()
    {
        projectileSpawnTransform = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        assetStore = new BlobAssetStore();
        world = World.DefaultGameObjectInjectionWorld;
        entityManager = world.EntityManager;
        GameObjectConversionSettings settings = GameObjectConversionSettings.FromWorld(world, assetStore);

        Entity enemyEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(enemyPrefab, settings);
        Entity neutralEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(neutralPrefab, settings);
        projectileEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(projectilePrefab, settings);
        spawnedAfterEnemyDeathEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(spawnedAfterEnemyDeathPrefab, settings);

        for (int i = 0; i < numberOfEnemyPrefabs; i++)
        {
            var instance = entityManager.Instantiate(enemyEntity);

            float x = UnityEngine.Random.Range(-500, 500);
            float y = UnityEngine.Random.Range(-100, 100);
            float z = UnityEngine.Random.Range(-500, 500);

            float3 position = new float3(x, y, z);
            entityManager.SetComponentData(instance, new Translation
            {
                Value = position
            });

            float randomSpeed = UnityEngine.Random.Range(1, 10) / 10f;
            entityManager.SetComponentData(instance, new MotionData
            {
                motionSpeed = randomSpeed
            });
        }

        for (int i = 0; i < numberOfNeutralPrefabs; i++)
        {
            var instance = entityManager.Instantiate(neutralEntity);

            float x = UnityEngine.Random.Range(-500, 500);
            float y = UnityEngine.Random.Range(-100, 100);
            float z = UnityEngine.Random.Range(-500, 500);

            float3 position = new float3(x, y, z);
            entityManager.SetComponentData(instance, new Translation
            {
                Value = position
            });

            float randomSpeed = UnityEngine.Random.Range(1, 10) / 10f;
            entityManager.SetComponentData(instance, new MotionData
            {
                motionSpeed = randomSpeed
            });
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for(int i = 0; i < numberOfProjectilesPerFire; i++)
            {
                Entity instance = entityManager.Instantiate(projectileEntity);

                Vector3 spawnPosition = projectileSpawnTransform.position + UnityEngine.Random.insideUnitSphere * 2;

                entityManager.SetComponentData(instance, new Translation
                {
                    Value = spawnPosition
                });

                entityManager.SetComponentData(instance, new Rotation
                {
                    Value = projectileSpawnTransform.rotation
                });
            }
        }
        
    }

    private void OnDestroy()
    {
        assetStore.Dispose();
    }
}
