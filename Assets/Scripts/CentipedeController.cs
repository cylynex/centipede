using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeController : MonoBehaviour {

    // head movement direction
    int currentDirection = 1;
    private int z;
	
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
        Debug.Log("hit something: "+coll.gameObject.name);
        transform.rotation = Quaternion.Euler(0, 0, 90);
        StartCoroutine("TurnBack");
    }


    IEnumerator TurnBack() {
        yield return new WaitForSeconds(0.07f);
        if (currentDirection == 1) {
            z = 0;
        } else {
            z = 180;
        }

        transform.rotation = Quaternion.Euler(0, 0, z);
    }


    // Movement
    void MoveLeft() {
        transform.Translate(Vector3.left * Time.deltaTime);
    }

    void MoveRight() {
        transform.Translate(Vector3.right * Time.deltaTime);
    }

}
