using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour {

    int hitPoints = 3;
    public Sprite damage1;
    public Sprite damage2;

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "LaserBeam") {
            Destroy(coll.gameObject);
            hitPoints--;

            if (hitPoints == 2) {
                GetComponent<SpriteRenderer>().sprite = damage1;
            } else if (hitPoints == 1) {
                GetComponent<SpriteRenderer>().sprite = damage2;
            }

            if (hitPoints <= 0) {
                Destroy(gameObject);
            }
        }
    }
}
