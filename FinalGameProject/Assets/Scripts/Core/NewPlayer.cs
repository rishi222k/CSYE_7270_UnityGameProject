using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewPlayer : PhysicsObject
{
    [SerializeField] private float maxSpeed = 1;
    [SerializeField] private float jumpPower = 10;
    [SerializeField] private float attackDuration;
    [SerializeField] private GameObject attackBox;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip deathSound;
    private bool frozen;

    public int attackPower = 25;
    private int jumpsRemaining = 1;
    public int coinsCollected;
    public int shardsCollected;
    public int keysCollected;
    public int RescueOps;
    //public Text coinsText;
    //public Image healthBar;

    public int maxHealth = 100;
    public int health = 100;
    [SerializeField] private Vector2 healthBarOrigSize;

    public AudioSource sfxAudioSource;
    public AudioSource musicAudioSource;
    public AudioSource ambienceAudioSource;

    //Singleton instantation
    private static NewPlayer instance;
    public static NewPlayer Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<NewPlayer>();
            return instance;
        }
    }

    private void Awake()
    {
        if (GameObject.Find("New Player")) Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        gameObject.name = "New Player";

        healthBarOrigSize = GameManager.Instance.healthBar.rectTransform.sizeDelta;
        UpdateUI();
        SetSpawnPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (!frozen)
        {
            targetVelocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, 0);

            if (grounded)
            {
                jumpsRemaining = 2;
            }

            // If the player presses "Jump" and there are jumps remaining, set the velocity to a jump power value and decrement jumpsRemaining
            if (Input.GetButtonDown("Jump") && jumpsRemaining > 0)
            {
                velocity.y = jumpPower;
                jumpsRemaining--;
            }

            //Flip the player's localScale.x if the move speed is greater than .01 or less than -.01
            if (targetVelocity.x < -.01)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else if (targetVelocity.x > .01)
            {
                transform.localScale = new Vector2(1, 1);
            }

            //If we press "Fire1", then set the attackBox to active. Otherwise, set active to false
            if (Input.GetButtonDown("Fire1"))
            {
                animator.SetTrigger("attack");
                StartCoroutine(ActivateAttack());
            }

            //Check if player health is smaller than or equal to 0.
            if (health <= 0)
            {
                StartCoroutine(Die());
            }
        }
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
        animator.SetFloat("velocityY", velocity.y);
        animator.SetBool("grounded", grounded);
        animator.SetFloat("attackDirectionY", Input.GetAxis("Vertical"));

    }

    //Activate Attack Function
    public IEnumerator ActivateAttack()
    {
        attackBox.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        attackBox.SetActive(false);
    }

    //Update UI elements
    public void UpdateUI()
    {
        GameManager.Instance.coinsText.text = coinsCollected.ToString();
        GameManager.Instance.shardsText.text = shardsCollected.ToString();
        GameManager.Instance.keysText.text = keysCollected.ToString();
        GameManager.Instance.RescueText.text = RescueOps.ToString();
        GameManager.Instance.healthBar.rectTransform.sizeDelta = new Vector2(healthBarOrigSize.x * ((float)health / (float)maxHealth), GameManager.Instance.healthBar.rectTransform.sizeDelta.y);
    }

    public void SetSpawnPosition()
    {
        transform.position = GameObject.Find("SpawnLocation").transform.position;
    }

    public IEnumerator Die()
    {
        frozen = true;
        sfxAudioSource.PlayOneShot(deathSound);
        animator.SetBool("dead", true);
        yield return new WaitForSeconds(2);
        LoadLevel();
        
    }

    public void LoadLevel()
    {
        animator.SetBool("dead", false);

        health = 100;
        coinsCollected=0;
        shardsCollected=0;
        keysCollected = 0;
        RescueOps = 6;
        frozen = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        SetSpawnPosition();
        UpdateUI();
    }
    public void HurtAnimation()
    {
        animator.SetTrigger("hurt");
    }

}