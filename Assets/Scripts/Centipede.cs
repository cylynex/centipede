using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centipede : MonoBehaviour {

    [Header("Infrastructure")]
    public GameObject pieceToFollow;
    public bool isHead = false;
    public float speed = 1;
    public bool firstBodyPart = false;

    public Sprite[] sprites;

    // Move Stuff
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


    void Start() {
        // Figure out if its a head or a body and assign the right sprite
        if (isHead) {
            GetComponent<SpriteRenderer>().sprite = sprites[0];
        } else {
            GetComponent<SpriteRenderer>().sprite = sprites[1];
        }
    }


    void Update() {
        if (isHead) {
            //MoveHead();
        }
    }


    void MoveHead() {
        /*if (movingDown == true) {
            TurnAround();
        }

        if (movingDownTurn == true) {
            if (transform.position.y <= downSpot.y) {
                TurnBack();
                movingDownTurn = false;
                avoidingShroom = false;
            }
        }

        Move();*/
    }

}
