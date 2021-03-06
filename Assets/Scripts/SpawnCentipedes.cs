﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCentipedes : MonoBehaviour {

    public GameObject[] spawnPoints;
    public GameObject centipedePrefab;
    public GameObject podPrefab;
    public List<GameObject> centiPods;
    public float timeBetweenSpawns = 15f;
    public float spawnTimer = 0;
    public float speedMod = 0.2f;
    public float baseSpeed = 0.8f;

    Transform spawnPodAt;
    Sprite spriteToUse;

    void Start() {

        SpawnCentipede();
        spawnTimer = timeBetweenSpawns;
    }

    void Update() {
        if (spawnTimer <= 0) {
            SpawnCentipede();
            spawnTimer = timeBetweenSpawns;
        } else {
            spawnTimer -= Time.deltaTime;
        }
    }


    void SpawnCentipede() {
        int spawnToUse = Random.Range(0, spawnPoints.Length);
        GameObject spawnPoint = spawnPoints[spawnToUse];
        GameObject centipede = Instantiate(centipedePrefab, spawnPoint.transform.position, Quaternion.identity);
        Vector3 spawnInitSpot = spawnPoint.transform.position;

        // Spawn some pods now inside that container
        int numberOfPods = Random.Range(6, 15);
        bool firstPod = true;
        bool secondPod = false;
        int podSpot = -1;


        spawnPodAt = spawnPoint.transform;
        for (int i = 0; i < numberOfPods; i++) {

            // Create the Pod and assign sprite
            GameObject thisPod = (GameObject)Instantiate(podPrefab, spawnPodAt.position, Quaternion.identity);
            if (firstPod) {
                thisPod.GetComponent<Centipede>().isHead = true;
                firstPod = false;
                secondPod = true;
            } else if (secondPod) {
                // First body segment
                thisPod.GetComponent<Centipede>().firstBodyPart = true;
                secondPod = false;
            } else {
                // Just another brick in the wall
            }

            // Set the speed Mod
            thisPod.GetComponent<Centipede>().speed = baseSpeed + speedMod;

            // Parent it to the CentiPod (hahahah i kill me)
            thisPod.transform.SetParent(centipede.transform);

            // Setup for the next pod spawn
            AdvanceSpawnPoint();

            // Add this pod to the list
            centiPods.Add(thisPod);

            // Assign the piece to follow
            if (podSpot >= 0) {
                thisPod.GetComponent<Centipede>().pieceToFollow = centiPods[podSpot];
            }

            // Increment the pod Counter
            podSpot++;

            // Reset the spawner which is moving for some reason
            spawnPoint.transform.position = spawnInitSpot;
        }

        // Wipe out the centipede list for next spawn
        centiPods.Clear();
        speedMod += 0.2f;
    }

    void AdvanceSpawnPoint() {
        float spawnX = spawnPodAt.position.x + 0.3f;
        spawnPodAt.position = new Vector3(spawnX, spawnPodAt.position.y, spawnPodAt.position.z);
    }
}
