using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HSoutput : MonoBehaviour {

    public Text highscore;
    public Text yourScore;

    void Start() {
        highscore.text = PlayerPrefs.GetInt("highscore").ToString();
        yourScore.text = PlayerPrefs.GetInt("yourscore").ToString();
    }
}
