using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMushrooms : MonoBehaviour {

    public GameObject mushroomPrefab;
    public int numberMushrooms;

    // Spawn Limits
    float minX = -5.5f;
    float maxX = 5.5f;
    float minY = -3f;
    float maxY = 4.5f;

    float xSpawn;
    float ySpawn;

    Vector3 spawnSpot;

	void Start () {
        for (int i = 0; i < numberMushrooms; i++) {
            xSpawn = Random.Range(minX, maxX);
            ySpawn = Random.Range(minY, maxY);
            spawnSpot = new Vector3(xSpawn, ySpawn, 0);
            Instantiate(mushroomPrefab, spawnSpot, Quaternion.identity);
        }
	}
	
	
}
