using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeController : MonoBehaviour {


	
    void Start() {
        Debug.Log("centipede initialized");
    }


    void Update() {
        transform.Translate(Vector3.left * Time.deltaTime);
    }

}
