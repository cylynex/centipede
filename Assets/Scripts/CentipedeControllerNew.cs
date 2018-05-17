using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeControllerNew : MonoBehaviour {

    // head movement direction
    [Header("Configurable")]
    public float speed = 0.5f;

    int currentDirection = 1;
    private int z;
    Vector3 downSpot;

    float xPos;
    float yPos;

    bool movingDown = false;
    bool avoidingShroom = false;
    bool movingDownTurn = false;

    [Header("For Body")]
    public List<Vector3> rotateAtPosition;
    public List<Quaternion> rotateTo;

    public Vector3 lastPosition;
    public Quaternion lastRotation;

    void Start() {
        // Establish position and rotation for pieces to follow
        // TODO rotation
        lastPosition = transform.position;
    }


    void Update() {
        if (movingDown == true) {
            TurnAround();
        }

        if (movingDownTurn == true) {
            if (transform.position.y <= downSpot.y) {
                TurnBack();
                movingDownTurn = false;
                avoidingShroom = false;
            }
        }

        Move();
    }


    void OnCollisionEnter2D(Collision2D coll) {

        // Wall
        if (coll.gameObject.name == "Wall") {
            Debug.Log("hit wall - turn around");
            transform.rotation = Quaternion.Euler(0, 0, 90);
            UpdateBodyRotation();

            // Set the target destination
            downSpot = transform.position;
            downSpot.y -= 0.4f;
            movingDown = true;
            TurnAround();
        }

        // Mushroom
        else if (coll.gameObject.tag == "Obstacle") {
            Debug.Log("hit obstacle");

            if (avoidingShroom) {
                Debug.Log("downward collision while avoiding shroom");
                // Reverse the polarity
                if (currentDirection == 1) {
                    currentDirection = 2;
                } else {
                    currentDirection = 1;
                }

                TurnBack();
                avoidingShroom = false;

            } else if (coll.gameObject.tag == "Centipede") {
                // Hit centipede
                Destroy(coll.gameObject);
                Destroy(gameObject);

            } else {
                Debug.Log("else collision");
                transform.rotation = Quaternion.Euler(0, 0, 90); 
                UpdateBodyRotation();

                // Set the target destination
                movingDownTurn = true;
                downSpot = transform.position;
                downSpot.y -= 0.4f;

                avoidingShroom = true;
            }
        } 
    }


    void TurnBack() {
        if (currentDirection == 1) {
            z = 0;
        } else {
            z = 180;
        }

        Debug.Log("turnback");
        transform.rotation = Quaternion.Euler(0, 0, z);
        UpdateBodyRotation();

    }

    void TurnAround() {
        if (transform.position.y <= downSpot.y) {
            if (currentDirection == 1) {
                currentDirection = 2;
                z = 180;
            } else {
                currentDirection = 1;
                z = 0;
            }

            Debug.Log("turnaround");
            transform.rotation = Quaternion.Euler(0, 0, z);
            UpdateBodyRotation();

            movingDown = false;
        }

    }


    // Movement
    void Move() {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }


    // Update variables for next body part
    void UpdateBodyRotation() {
        rotateAtPosition.Add(transform.position);
        rotateTo.Add(transform.rotation);
    }


    // Check next
    public bool CheckNextMove(int spotToCheck) {
        int correctedSpotCheck = spotToCheck + 1;
        if (rotateAtPosition.Count < correctedSpotCheck) return false;
        return true;
    }
}
