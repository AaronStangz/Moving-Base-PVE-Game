using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    public List<GameObject> Spawnpoints;
    public List<GameObject> Spawning;
    public float SpawnArea = 5;

    void Start()
    {
        foreach (GameObject p in Spawnpoints)
        {
            Vector3 spawnPoint = new Vector3(Random.Range(-SpawnArea, SpawnArea), 0, Random.Range(-SpawnArea, SpawnArea));
            Instantiate(Spawning[Random.Range(0, Spawning.Count)], p.transform.position + spawnPoint, p.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
