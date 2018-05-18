using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public static int points = 0;

    public Text scoreBoard;
    public Text livesBoard;

    Vector3 mousePos;
    int offset = 8;
    float maxFireCountdown = 0.5f;
    float fireCountdown;
    public GameObject firePoint;
    public GameObject laser;
    public float thrust = .1f;
    public int lives = 3;

    void Start() {
        points = 0;

        fireCountdown = 0;
    }

    void Update() {
        
        MoveWithMouse();

        CheckForShoot();

        UpdateScore();
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


    void UpdateScore() {
        scoreBoard.text = points.ToString();
    }
	
}
