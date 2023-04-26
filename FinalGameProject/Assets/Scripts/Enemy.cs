using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PhysicsObject
{
    [SerializeField] private float maxSpeed;
    private int direction = 1;
    public int health = 100;
    //private int maxHealth = 100;

    private RaycastHit2D rightLedgeRaycastHit;
    private RaycastHit2D leftLedgeRaycastHit;
    private RaycastHit2D rightWallRaycastHit;
    private RaycastHit2D leftWallRaycastHit;
    [SerializeField] private ParticleSystem particleExplosion;
    [SerializeField] private GameObject CollectableObject;
    [SerializeField] private LayerMask rayCastLayerMask;

    [SerializeField] private Vector2 rayCastOffset;
    [SerializeField] private float rayCastLength = 2;
    [SerializeField] private int attackPower = 15;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private float hurtSoundVolume = 1;
    [SerializeField] private float deathSoundVolume = 1;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = new Vector2(maxSpeed * direction, 0);

        //Check for right ledge!
        rightLedgeRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x + rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.down, rayCastLength);
        Debug.DrawRay(new Vector2(transform.position.x + rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.down * rayCastLength, Color.blue);
        if (rightLedgeRaycastHit.collider == null) direction = -1;

        //Check for left ledge!
        leftLedgeRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x - rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.down, rayCastLength);
        Debug.DrawRay(new Vector2(transform.position.x - rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.down * rayCastLength, Color.green);
        if (leftLedgeRaycastHit.collider == null) direction = 1;

        //Check for right wall!
        rightWallRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, rayCastLength, rayCastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.right * rayCastLength, Color.red);
        if (rightWallRaycastHit.collider != null) direction = -1;

        //Check for left wall!
        leftWallRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, rayCastLength, rayCastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.left * rayCastLength, Color.magenta);
        if (leftWallRaycastHit.collider != null) direction = 1;

        if (health <= 0)
        {
            NewPlayer.Instance.sfxAudioSource.PlayOneShot(deathSound, deathSoundVolume);
            particleExplosion.transform.parent = null;
            particleExplosion.gameObject.SetActive(true);
            if (CollectableObject)
            {
                CollectableObject.transform.parent = null;
                CollectableObject.SetActive(true);
            }
            Destroy(particleExplosion.gameObject, particleExplosion.main.duration);
            Destroy(gameObject);
        }
    }

    public void Hurt(int attackPower)
    {
        health -= attackPower;
        NewPlayer.Instance.sfxAudioSource.PlayOneShot(hurtSound, hurtSoundVolume);
    }

    //If I collide with the player, hurt the player (health is going to decrease, update the UI)
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject == NewPlayer.Instance.gameObject)
        {
            NewPlayer.Instance.HurtAnimation();
            Debug.Log("Hurt the player!");
            NewPlayer.Instance.health -= attackPower;
            NewPlayer.Instance.UpdateUI();
        }
    }
}
