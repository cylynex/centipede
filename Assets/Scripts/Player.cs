using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public static int points = 0;

    public Text scoreBoard;
    public Text livesBoard;
    public Text highScoreBoard;

    public AudioSource[] sounds;

    Vector3 mousePos;
    int offset = 8;
    float maxFireCountdown = 0.3f;
    float fireCountdown;
    public GameObject firePoint;
    public GameObject laser;
    public ParticleSystem explosionPrefab;
    public float thrust = .1f;
    public int lives = 3;
    static int highScore;

    void Start() {
        PlayerPrefs.SetInt("yourscore", 0);
        highScore = PlayerPrefs.GetInt("highscore");
        highScoreBoard.text = highScore.ToString();

        points = 0;
        fireCountdown = 0;
        lives = 3;
        livesBoard.text = lives.ToString();
    }

    void Update() {
        
        MoveWithMouse();

        CheckForShoot();

        UpdateScore();
    }


    void MoveWithMouse() {
        Vector3 playerPos = transform.position;
        float mousePosInBlocks = Input.mousePosition.x / Screen.width * 16 - offset;
        playerPos.x = Mathf.Clamp(mousePosInBlocks, -6f, 6f);
        transform.position = playerPos;
    }


    void CheckForShoot() {
        if (fireCountdown >= 0) { fireCountdown -= Time.deltaTime; }

        if (fireCountdown <= 0) {
            if (Input.GetMouseButtonDown(0)) {
                sounds[1].Play();
                GameObject theLaser = (GameObject)Instantiate(laser, firePoint.transform.position, Quaternion.identity);
                theLaser.GetComponent<Rigidbody2D>().AddForce(transform.up * thrust);
                fireCountdown = maxFireCountdown;
            }
        }
    }


    void UpdateScore() {
        scoreBoard.text = points.ToString();
        if (points > highScore) {
            PlayerPrefs.SetInt("highscore",points);
        }
        PlayerPrefs.SetInt("yourscore",points);
        highScoreBoard.text = points.ToString();
    }


    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag == "Obstacle") {
            ParticleSystem blowup = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            blowup.Play();
            sounds[0].Play();
            Destroy(blowup, 3f);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            lives--;
            livesBoard.text = lives.ToString();
            if (lives > 0) {

                GameObject[] clears = GameObject.FindGameObjectsWithTag("CentiPod");
                foreach (GameObject clear in clears) {
                    Destroy(clear);
                }

                StartCoroutine("Respawn");
            } else {
                SceneManager.LoadScene("Lose");
            }
        } else {
            Debug.Log("nottaggedright");
        }
    }


    IEnumerator Respawn() {
        yield return new WaitForSeconds(5);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        transform.position = new Vector3(0f, -4.4f, 0f);
    }
	
}
