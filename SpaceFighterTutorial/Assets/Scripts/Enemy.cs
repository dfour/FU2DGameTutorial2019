using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;    // Follow speed

    [HideInInspector]
    public bool hasTarget = false;  // do I have a target to move towards
    [HideInInspector]
    public GameObject target;   // the target i want to get closer to 

    private Rigidbody2D rb;

    void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (hasTarget) {
            //get distance between me and my target
            float distance = Vector3.Distance(transform.position, target.transform.position);
            // am I further than 2 units away
            if (distance > 2) {
                // I am over 2 units away
                follow(target.transform); // do a follow
            }
        }
    }

    // if anything starts to collide with me I will run this method
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name.Equals("PlayerObject")) {    // is the other object the player
            target = collision.gameObject;      // it is so set him as my target
            hasTarget = true;   // I have a target
        }
    }

    // if something is no longer coliiding with me I will run this code
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.name.Equals("PlayerObject")) {
            target = null;
            hasTarget = false;
        }
    }

    private void follow(Transform target) {
        // add force to my rigid body to make me move
        rb.AddForce((target.transform.position - transform.position).normalized * speed);
    }
}
