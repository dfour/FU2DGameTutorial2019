using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float hitPoints = 100f;
    private Rigidbody2D rb;
    public GameObject bulletPrefab;     // the prefab of our bullet

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        if(rb == null) {
            Debug.LogError("Player::Start cant find RigidBody2D </sadface>");
        }
    }
    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)) {
            // if the player pressed space (exclude holding key down)
            GameObject go = Instantiate(bulletPrefab, gameObject.transform);
            Bullet bullet = go.GetComponent<Bullet>();
            bullet.targetVector = new Vector3(1,1,0);
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
}
