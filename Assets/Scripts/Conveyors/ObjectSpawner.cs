using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject toSpawn;

    public Vector3 spawnOffset;
    
    public float spawnTimer = 5f;

    private ConveyorManager manager;

    private void Start()
    {
        manager = GameObject.FindWithTag("TileManager").GetComponent<ConveyorManager>();
        StartCoroutine(SpawnObject());
    }
    
    public IEnumerator SpawnObject()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTimer);
            if (manager.isRunning)
            {
                Instantiate(toSpawn, transform.position + spawnOffset, Quaternion.identity);
            }
        }
        
    }
}
