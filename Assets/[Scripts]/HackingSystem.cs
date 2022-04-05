using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingSystem : MonoBehaviour
{
    // Tile prefab
    public GameObject tileGameObject;

    // Movement speed
    [Header("Movement Speed")]
    public float movementSpeed = -5f;

    // Spawn point
    [Header("Spawn Point")] 
    public List<Transform> spawnPoints;

    public float spawnDelay = 2f;
    public float spawnTimeCounter = 0f;

    public bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            spawnTimeCounter += Time.deltaTime;

            if (spawnTimeCounter >= spawnDelay)
            {
                spawnTimeCounter = 0f;
                SpawnTiles();
            }
        }
    }


    /// <summary>
    /// Spawns the tiles from 4 spawn points
    /// </summary>
    private void SpawnTiles()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            GameObject tempTile = Instantiate(tileGameObject, spawnPoint.position, Quaternion.identity);
            tempTile.GetComponent<Transform>().parent = spawnPoint;
            TileBehaviour tile = tempTile.GetComponent<TileBehaviour>();
            tile.movementSpeed = movementSpeed;
            
        }
    }
}
