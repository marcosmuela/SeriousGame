using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedObjectSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Lista de objetos a elegir
    public float initialSpawnTime = 1f; // Tiempo inicial antes de aparecer
    private int spawnedCount = 0;
    private bool isSpawning = true;

    void Start()
    {
        for (int i = 0; i < objectsToSpawn.Length; i++)
        {
            Invoke("SpawnObject", initialSpawnTime * (i + 1));
        }
    }

    void SpawnObject()
    {
        if (objectsToSpawn.Length > 0 && isSpawning && spawnedCount < objectsToSpawn.Length)
        {
            Instantiate(objectsToSpawn[spawnedCount], transform.position, transform.rotation);
            spawnedCount++;
            if (spawnedCount >= objectsToSpawn.Length)
            {
                isSpawning = false;
            }
        }
    }
}


