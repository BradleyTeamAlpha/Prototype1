using System;
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
        //InvokeRepeating("SpawnObject", 0, spawnTimer);
        StartCoroutine(SpawnObject());
    }
    
    public IEnumerator SpawnObject()
    {
        while (true)
        {
            //Debug.Log($"isRunning: {manager.isRunning}");
            if (manager.isRunning)
            {
                //Debug.Log("Spawning");
                Instantiate(toSpawn, transform.position + spawnOffset, Quaternion.identity);
            }
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
