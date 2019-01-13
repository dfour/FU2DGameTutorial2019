using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject playerObject;  // the player object to follow
    public float lerpSpeed = 0.5f;
    private Vector3 offset;

    void Start() {
        // get the currentr offset between player and camarea positions
        offset = transform.position - playerObject.transform.position;
    }

    void LateUpdate() {
        transform.position = Vector3.Lerp(
            transform.position,                         // current camera position
            playerObject.transform.position + offset,   // new position plus our original offset
            lerpSpeed);                                 // the speed of smoothing
    }
}
