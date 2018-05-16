using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour {

    public GameObject pieceToFollow;


    void Update() {

        Vector3 incomingPosition = pieceToFollow.GetComponent<CentipedeControllerNew>().lastPosition;

        Debug.Log("my loc: " + transform.position);
        Debug.Log("incoming position: " + incomingPosition);

        Vector3 newPosition = incomingPosition - transform.position;
        Debug.Log("next stop: " + newPosition);

        transform.position = newPosition;


        /*
        transform.rotation = pieceToFollow.GetComponent<CentipedeControllerNew>().lastRotation;
        transform.position = pieceToFollow.GetComponent<CentipedeControllerNew>().lastPosition;
        */
    }
}
