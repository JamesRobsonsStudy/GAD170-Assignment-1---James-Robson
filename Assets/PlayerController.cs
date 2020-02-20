using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Script: PlayerController
    Author: Gareth Lockett
    Version: 1.0
    Description: Simple player controller script. Use arrow keys to move and spacebar to jump.
*/

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;        // Distance moved (units per second) when user holds down up or down arrow.
    public float turnSpeed = 60f;       // Rotating speed (degrees per second) when user holds down left or right arrow.
    public float jumpHeight = 5f;       // Upward velocity when user presses spacebar.

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Check up and down keys to move forwards or backwards.
        if (Input.GetKey(KeyCode.UpArrow) == true) { this.transform.position += this.transform.forward * Time.deltaTime * this.moveSpeed; }
        if (Input.GetKey(KeyCode.DownArrow) == true) { this.transform.position -= this.transform.forward * Time.deltaTime * this.moveSpeed; }

        // Check left and right keys to rotate left and right.
        if (Input.GetKey(KeyCode.LeftArrow) == true) { this.transform.Rotate(this.transform.up, Time.deltaTime * -this.turnSpeed); }
        if (Input.GetKey(KeyCode.RightArrow) == true) { this.transform.Rotate(this.transform.up, Time.deltaTime * this.turnSpeed); }

        // Check spacebar to trigger jumping. Checks if vertical velocity (eg velocity.y) is near to zero.
        if (Input.GetKey(KeyCode.Space) == true && Mathf.Abs(this.GetComponent<Rigidbody>().velocity.y) < 0.01f)
        {
            this.GetComponent<Rigidbody>().velocity += Vector3.up * this.jumpHeight;
        }
    }
}
