using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour {

    public GameObject pieceToFollow;
    float speed;
    int moveIndex = 0;
    bool firstPiece = false;

    public Vector3 nextTurn;
    public Quaternion nextRotation;

    [Header("For Body")]
    public List<Vector3> rotateAtPosition;
    public List<Quaternion> rotateTo;

    void Update() {

        GetNextManeuver();
        if (transform.position == nextTurn) {
            transform.rotation = nextRotation;
            moveIndex++;
        }

        Move();

        /*
        transform.rotation = pieceToFollow.GetComponent<CentipedeControllerNew>().lastRotation;
        transform.position = pieceToFollow.GetComponent<CentipedeControllerNew>().lastPosition;
        */
    }


    // Movement
    void Move() {
        //transform.Translate(Vector3.left * Time.deltaTime * speed);
        transform.position = Vector3.MoveTowards(transform.position, nextTurn, Time.deltaTime);
        if (transform.position == nextTurn) {
            Debug.Log("here");
        }
    }


    void GetNextManeuver() {
        if (pieceToFollow.GetComponent<CentipedeControllerNew>().CheckNextMove(moveIndex)) {
            Debug.Log("has a next move");
            nextTurn = pieceToFollow.GetComponent<CentipedeControllerNew>().rotateAtPosition[moveIndex];
            nextRotation = pieceToFollow.GetComponent<CentipedeControllerNew>().rotateTo[moveIndex];
        } else {
            Debug.Log("no next move available - do something else");
            // Go to the heads direction instead
            nextTurn = pieceToFollow.GetComponent<CentipedeControllerNew>().transform.position;
            foreach (Transform child in pieceToFollow.transform) {
                if (child.tag == "followSpot") {
                    transform.position = child.position;
                }
            }
        }

    }

}
