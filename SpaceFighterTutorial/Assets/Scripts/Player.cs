using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IEntityHealth
{
    public float moveSpeed = 5f;
    public float hitpoints = 100f;
    private Rigidbody2D rb;

    private bool isDead = false;


    public ScObWeapon currentWeapon;
    private float lastFired = 0f;       // last shot time
    private float reloadTimer = 0f;     // time until reload finsihes
    private int magazine;               // current bullet count
    private bool reloading = false;      // is reloading

    //public GameObject bulletPrefab;     // the prefab of our bullet

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        if(rb == null) {
            Debug.LogError("Player::Start cant find RigidBody2D </sadface>");
        }
        reloadTimer = currentWeapon.reloadSpeed;
        magazine = currentWeapon.magazineCapacity;
    }
    // Update is called once per frame
    void Update(){
        if (Input.GetMouseButton(0)) {
            fire();     // do us a firing!
        }
        lastFired -= Time.deltaTime;        // reduce last fired Timer
        if (reloading) {                    // check if were reloading
            reloadTimer -= Time.deltaTime;  // lower reload timer
            if (reloadTimer <= 0) {         // have we reloaded long enough
                reloading = false;          // reset reload
                magazine = currentWeapon.magazineCapacity;// refill our pew pew machine
            }
        }
    }

    // this is called at a fixed interval for use with physics objects like the RigidBody2D
    void FixedUpdate() {
        // check if user has pressed some input keys
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {

            // convert user input into world movement
            float horizontalMovement = Input.GetAxisRaw("Horizontal") * moveSpeed;
            float verticalMovement = Input.GetAxisRaw("Vertical") * moveSpeed ;

            //assign world movements to a Veoctor2
            Vector2 directionOfMovement = new Vector2(horizontalMovement, verticalMovement);

            // apply movement to player's transform
            rb.AddForce(directionOfMovement);
        }
    }

    // allows public access to amount of bullets left
    public float getMagazine() {
        return magazine;
    }

    public bool isReloading() {
        return reloading;
    }

    private void startReload() {
        reloading = true;
        reloadTimer = currentWeapon.reloadSpeed;
    }

    public void fire() {
        if (lastFired <= 0f && !reloading) {    // make sure we dont shoot while reloading or too often
            if (magazine == 1) {    // this is our last bullet
                startReload();      // start reloading
            }
            Vector3 pointMouseVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pointMouseVector.z = 0; // set z to 0, this is 2D
            GameObject go = Instantiate(currentWeapon.bulletPrefab, gameObject.transform.position, Quaternion.identity);
            Bullet bullet = go.GetComponent<Bullet>();
            go.layer = 8; // set out bullet to the player layer
            Vector3 targetVector = pointMouseVector - gameObject.transform.position;
            bullet.targetVector = targetVector;
            lastFired = currentWeapon.fireRate; // we just fired, add a delay with lastFired timer
            magazine -= 1;  // bye bye bullet
        }
    }

    public void IGainHealth(float health) {
        // do nothing yet
    }

    public void ITakeDamage(float damage) {
        hitpoints -= damage;
        if (hitpoints <= 0) {
            isDead = true;
            Debug.Log("Our Player died, do nothing for now");
        }
    }
}
