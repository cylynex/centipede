using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeController : MonoBehaviour {

    // head movement direction
    [Header("Configurable")]
    public float speed = 0.5f;

    int currentDirection = 1;
    private int z;
    public int thisHeadTag;

    [Header("For Body")]
    public Vector3 lastPosition;
    public Quaternion lastRotation;

    void Start() {
    }


    void Update() {
        if (currentDirection == 1) {
            MoveLeft();
        } else {
            MoveRight();
        }

    }


    void OnCollisionEnter2D(Collision2D coll) {

        // Wall
        if (coll.gameObject.name == "Wall") {
            Debug.Log("hit wall - turn around");
            lastRotation = transform.rotation;
            transform.rotation = Quaternion.Euler(0, 0, 90);
            StartCoroutine("TurnAround");
        }

        // Mushroom
        else if (coll.gameObject.tag == "Obstacle") {
            Debug.Log("hit obstacle");
            lastRotation = transform.rotation;
            transform.rotation = Quaternion.Euler(0, 0, 90);
            StartCoroutine("TurnBack");
        }
    }


    IEnumerator TurnBack() {
        yield return new WaitForSeconds(0.1f);
        if (currentDirection == 1) {
            z = 0;
        } else {
            z = 180;
        }
        lastRotation = transform.rotation;
        transform.rotation = Quaternion.Euler(0, 0, z);
    }

    IEnumerator TurnAround() {
        yield return new WaitForSeconds(0.1f);
        if (currentDirection == 1) {
            currentDirection = 2;
            z = 180;
        } else {
            currentDirection = 1;
            z = 0;
        }
        lastRotation = transform.rotation;
        transform.rotation = Quaternion.Euler(0, 0, z);
    }


    // Movement
    void MoveLeft() {
        lastPosition = transform.position;
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

    void MoveRight() {
        lastPosition = transform.position;
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

}
