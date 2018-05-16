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
            transform.rotation = Quaternion.Euler(0, 0, 90);
            StartCoroutine("TurnAround");
        }

        // Mushroom
        else if (coll.gameObject.tag == "Obstacle") {
            Debug.Log("hit obstacle");
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

        transform.rotation = Quaternion.Euler(0, 0, z);
    }


    // Movement
    void MoveLeft() {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

    void MoveRight() {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

}
