using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEntityHealth
{
    public float speed = 2f;    // Follow speed

    [HideInInspector]
    public bool hasTarget = false;  // do I have a target to move towards
    [HideInInspector]
    public GameObject target;   // the target i want to get closer to 

    public float hitpoints = 100f;
    private bool isDead = false; 

    private Rigidbody2D rb;

    public ScObWeapon currentWeapon;

    private float lastFired = 0f;       // last shot time
    private float reloadTimer = 0f;     // time til reload finsihes
    private int magazine;               // current bullet count
    private bool reloading = true;      // is reloading

 

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
            shoot();
        }

        if (isDead) {
            // I am dead, destroy me
            Destroy(gameObject);
        }
    }

    // allow our enemy to shoot
    private void shoot() {
        // same as player (ideally we would have this in its own class(like entityWeapon) but for now
        // we will just add it here and give it a TODO
        // TODO move into own class as this is shared between player and enemy
        lastFired -= Time.deltaTime;
        if (reloading) {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0) {
                reloading = false;
                magazine = currentWeapon.magazineCapacity;
            }
        } else {
            if (lastFired <= 0f && !reloading) {

                if (magazine == 1) {
                    startReload();
                }

                Vector3 direction = (target.transform.position - transform.position).normalized;

                GameObject bulletObject = Instantiate(currentWeapon.bulletPrefab, (transform.position + (direction * 0.6f)), Quaternion.identity);
                bulletObject.layer = 9;
                Bullet bullet = bulletObject.GetComponent<Bullet>();
                bullet.targetVector = direction.normalized;
                bulletObject.transform.position = transform.position;
                lastFired = currentWeapon.fireRate;

                magazine -= 1;
            }

        }
    }

    private void startReload() {
        reloading = true;
        reloadTimer = currentWeapon.reloadSpeed;
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

    public void IGainHealth(float health) {
        // do nothing yet
    }

    public void ITakeDamage(float damage) {
        hitpoints -= damage;
        if (hitpoints <= 0) {
            isDead = true;
        }
    }
}
