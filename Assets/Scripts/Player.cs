using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Vector3 mousePos;
    int offset = 8;
    float maxFireCountdown = 3f;
    public float fireCountdown;

    void Start() {
        fireCountdown = maxFireCountdown;
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
                Debug.Log("fire");
                fireCountdown = maxFireCountdown;
            }
        }
    }
	
}
