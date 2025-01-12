using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    public List<GameObject> LagerRockSpawnpoints;
    public List<GameObject> LagerRockPrefab;
    public float SpawnArea = 1000;

    void Start()
    {
        foreach (GameObject point in LagerRockSpawnpoints)
        {
            // Random offset within SpawnArea
            Vector3 randomOffset = new Vector3(
                Random.Range(-SpawnArea, SpawnArea),
                0f,
                Random.Range(-SpawnArea, SpawnArea)
            );

            // Random rotation around the Y-axis
            Vector3 randomRot = new Vector3(0f, Random.Range(0f, 0f), 360f);

            // Choose a random prefab from the Spawning list
            GameObject prefabToSpawn = LagerRockPrefab[Random.Range(0, LagerRockPrefab.Count)];

            // Instantiate the prefab on the server
            GameObject spawnedObject = Instantiate(
                prefabToSpawn,
                point.transform.position + randomOffset,
                Quaternion.Euler(randomRot)
            );

            // Spawn the object over the network
            Instantiate(spawnedObject);
        }
    }

        // Update is called once per frame
    void Update()
    {
        
    }
}
