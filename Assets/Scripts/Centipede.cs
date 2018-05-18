using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centipede : MonoBehaviour {

    [Header("Infrastructure")]
    public GameObject pieceToFollow;
    public bool isHead = false;
    public float speed = 2;
    public bool firstBodyPart = false;
    public GameObject obstaclePrefab;
    public int pointValue = 10;

    public Sprite[] sprites;

    // Move Stuff
    int readMoveIndex = 0;
    int moveIndex = 0;
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
    public Vector3 nextTurn;
    public Quaternion nextRotation;
    Vector3 amIstuck;


    void Start() {
        // Figure out if its a head or a body and assign the right sprite
        if (isHead) {
            GetComponent<SpriteRenderer>().sprite = sprites[0];
        } else {
            GetComponent<SpriteRenderer>().sprite = sprites[1];
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }


    void Update() {
        if (isHead) {
            MoveHead();
        } else {
            MoveBody();
        }
    }


    // Head Functions and Movement 
    void MoveHead() {

        if (movingDown == true) {
            Debug.Log("movingdown");
            TurnAround();
        }

        if (movingDownTurn == true) {
            Debug.Log("movingdownturn");
            if (transform.position == amIstuck) {
                GetUnstuck();
            }
            amIstuck = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            if (transform.position.y <= downSpot.y) {
                TurnBack();
                movingDownTurn = false;
                avoidingShroom = false;
            }
        }


        MoveFront();

    }


    // Get UnStuck
    void GetUnstuck() {
        movingDown = false;
        int randDir = Random.Range(1, 2);
        if (randDir == 1) {
            currentDirection = 1;
            z = 0;
        } else {
            currentDirection = 2;
            z = 180;
        }

        transform.rotation = Quaternion.Euler(0, 0, z);
        UpdateBodyRotation();

        movingDown = false;
        avoidingShroom = false;
        movingDownTurn = false;
    }

    /* Head Movement Specific Stuff */
    void MoveFront() {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }


    void TurnBack() {
        if (currentDirection == 1) {
            z = 0;
        } else {
            z = 180;
        }

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

            transform.rotation = Quaternion.Euler(0, 0, z);
            UpdateBodyRotation();

            movingDown = false;
        } 
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


    // Collisions for head control
    void OnCollisionEnter2D(Collision2D coll) {
        if (isHead) {
            // Wall
            if (coll.gameObject.name == "Wall") {
                transform.rotation = Quaternion.Euler(0, 0, 90);
                UpdateBodyRotation();

                // Set the target destination
                downSpot = transform.position;
                downSpot.y -= 0.4f;
                movingDown = true;
                TurnAround();
            }

            // Hit an Obstacle (non-wall)
            else if (coll.gameObject.tag == "Obstacle") {

                if (avoidingShroom) {
                    // Reverse the polarity (downward collision while already avoiding an obstacle
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
                    // Else collision
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
    }




    /* Body Movement Specific Stuff */


    // Body Movement
    void MoveBody() {

        // Check if it lost its way (bad head)
        if (pieceToFollow == null) { 
            Debug.Log("lost head"); 
            isHead = true;
            GetComponent<BoxCollider2D>().isTrigger = false;
            GetComponent<SpriteRenderer>().sprite = sprites[0];

            //  crazy ivan
            if (currentDirection == 1) {
                currentDirection = 2;
            } else {
                currentDirection = 1;
            }

            TurnBack();

            /*
            transform.rotation = Quaternion.Euler(0, 0, 90);
            UpdateBodyRotation();

            // Set the target destination
            movingDownTurn = true;
            downSpot = transform.position;
            downSpot.y -= 0.4f;

            avoidingShroom = true;

            if (currentDirection == 1) { currentDirection = 2; } else { currentDirection = 1; }
            */
        }

        GetNextManeuver();
        if (transform.position == nextTurn) {
            transform.rotation = nextRotation;

            // Update internal list
            rotateAtPosition.Add(pieceToFollow.GetComponent<Centipede>().rotateAtPosition[moveIndex]);
            rotateTo.Add(pieceToFollow.GetComponent<Centipede>().rotateTo[moveIndex]);

            moveIndex++;
        }

        MoveAss();
    }


    // Movement for body only
    void MoveAss() {
        transform.position = Vector3.MoveTowards(transform.position, nextTurn, speed * Time.deltaTime);
    }


    void GetNextManeuver() {
        if (pieceToFollow != null) {
            if (pieceToFollow.GetComponent<Centipede>().CheckNextMove(moveIndex)) {
                nextTurn = pieceToFollow.GetComponent<Centipede>().rotateAtPosition[moveIndex];
                nextRotation = pieceToFollow.GetComponent<Centipede>().rotateTo[moveIndex];
            } else {
                // Go to the heads direction instead
                nextTurn = pieceToFollow.GetComponent<Centipede>().transform.position;
                foreach (Transform child in pieceToFollow.transform) {
                    if (child.tag == "followSpot") {
                        transform.position = child.position;
                    }
                }
            }
        }
    }


    // Get Laser'ed
    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.tag == "LaserBeam") {
            GetComponent<AudioSource>().Play();
            Transform popSpot = transform;
            Destroy(coll.gameObject);
            Destroy(gameObject);
            Instantiate(obstaclePrefab, popSpot.position, Quaternion.identity);
            Player.points += pointValue;
        }
    }

}
