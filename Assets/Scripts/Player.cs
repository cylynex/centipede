using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Vector3 mousePos;
    int offset = 8;
    float maxFireCountdown = 1f;
    float fireCountdown;
    public GameObject firePoint;
    public GameObject laser;
    float thrust = 150;

    void Start() {
        fireCountdown = 0;
    }

    void Update() {
        
        MoveWithMouse();

        CheckForShoot();

    }


    void MoveWithMouse() {
        Vector3 playerPos = transform.position;
        float mousePosInBlocks = Input.mousePosition.x / Screen.width * 16 - offset;
        playerPos.x = Mathf.Clamp(mousePosInBlocks, -6f, 6f);
        transform.position = playerPos;
    }


    void CheckForShoot() {
        if (fireCountdown >= 0) { fireCountdown -= Time.deltaTime; }

        if (fireCountdown <= 0) {
            if (Input.GetMouseButtonDown(0)) {
                GameObject theLaser = (GameObject)Instantiate(laser, firePoint.transform.position, Quaternion.identity);
                theLaser.GetComponent<Rigidbody2D>().AddForce(transform.up * thrust);
                fireCountdown = maxFireCountdown;
            }
        }
    }
	
}
