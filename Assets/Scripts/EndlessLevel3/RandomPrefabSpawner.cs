using UnityEngine;
using VContainer;

public class RandomPrefabSpawner : MonoBehaviour
{
    public GameObject[] PrefabList;
    public Transform[] SpawnPoints;
    
    [Inject]
    private readonly PrefabInject _prefabInject;

    private void Start()
    {
        SpawnRandomPrefabs();
    }

    private void SpawnRandomPrefabs()
    {
        if (PrefabList.Length == 0 || SpawnPoints.Length == 0)
        {
            Debug.LogWarning("Prefab list or spawn points not set.");
            return;
        }

        Transform[] availableSpawnPoints = new Transform[SpawnPoints.Length];
        SpawnPoints.CopyTo(availableSpawnPoints, 0);

        int prefabIndex = 0;

        for (int i = 0; i < Mathf.Min(PrefabList.Length, SpawnPoints.Length); i++)
        {
            if (prefabIndex >= PrefabList.Length)
                prefabIndex = 0;

            GameObject prefabToSpawn = PrefabList[prefabIndex];

            if (availableSpawnPoints.Length > 0)
            {
                int randomSpawnPointIndex = Random.Range(0, availableSpawnPoints.Length);
                Transform spawnPoint = availableSpawnPoints[randomSpawnPointIndex];
                var spawn = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
                _prefabInject.InjectGameObject(spawn);

                availableSpawnPoints[randomSpawnPointIndex] = availableSpawnPoints[availableSpawnPoints.Length - 1];
                System.Array.Resize(ref availableSpawnPoints, availableSpawnPoints.Length - 1);
            }

            prefabIndex++;
        }
    }
}