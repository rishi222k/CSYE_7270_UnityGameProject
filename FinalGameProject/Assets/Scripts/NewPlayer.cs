using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewPlayer : PhysicsObject
{
    [SerializeField] private float maxSpeed = 1;
    [SerializeField] private float jumpPower = 10;
    private int jumpsRemaining = 1;
    public int coinsCollected;
    public int shardsCollected;
    public int keysCollected;
    public Text coinsText;

    public int maxHealth = 100;
    public int health = 100;
    public Image healthBar;
    [SerializeField] private Vector2 healthBarOrigSize;

    // Start is called before the first frame update
    void Start()
    {
        healthBarOrigSize = healthBar.rectTransform.sizeDelta;
        UpdateUI();
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
    //Update UI elements
    public void UpdateUI()
    {
        coinsText.text = coinsCollected.ToString();
        healthBar.rectTransform.sizeDelta = new Vector2(healthBarOrigSize.x * ((float)health / (float)maxHealth), healthBar.rectTransform.sizeDelta.y);
    }
}