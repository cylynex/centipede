using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splashload : MonoBehaviour {

    void Start() {
        StartCoroutine("LoadGame");
    }


    IEnumerator LoadGame() {
        yield return new WaitForSeconds(24);
        SceneManager.LoadScene("Menu");
    }
}
