using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayer : PhysicsObject
{
    [SerializeField] private float maxSpeed = 1;
    [SerializeField] private float jumpPower = 10;
    private int jumpsRemaining = 1;
    public int coinsCollected;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, 0);

        if (grounded)
        {
            jumpsRemaining = 1;
        }


        // If the player presses "Jump" and there are jumps remaining, set the velocity to a jump power value and decrement jumpsRemaining
        if (Input.GetButtonDown("Jump") && jumpsRemaining > 0)
        {
            velocity.y = jumpPower;
            jumpsRemaining--;
        }

    }
}