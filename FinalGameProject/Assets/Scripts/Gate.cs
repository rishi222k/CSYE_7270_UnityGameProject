using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private int requiredKeys;
    [SerializeField] private int requiredShards;
    [SerializeField] private int requiredCoins;
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CanOpen(int playerKeys, int playerShards, int playerCoins)
    {
        return playerKeys >= requiredKeys && playerShards >= requiredShards && playerCoins >= requiredCoins; 
    }

    public void Open(int playerKeys, int playerShards, int playerCoins)
    {
        if (CanOpen(playerKeys, playerShards,playerCoins))
        {
            // Deduct the required keys and shards from the player's inventory
            NewPlayer.Instance.keysCollected -= requiredKeys;
            NewPlayer.Instance.shardsCollected -= requiredShards;
            NewPlayer.Instance.coinsCollected -= requiredCoins;

            // Open the gate, perform your desired action here, e.g., animation or sound
            Debug.Log("Gate opened!");
            NewPlayer.Instance.UpdateUI();
            animator.SetBool("opened", true);
            animator.SetBool("gateEntry", false);
            StartCoroutine(Destroy());
        }
        else
        {
            Debug.Log("Not enough keys or shards to open the gate.");
        }
    }
    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player is touching me, check if the gate can be opened and open if possible
        if (collision.gameObject == NewPlayer.Instance.gameObject)
        {
            Gate gate = GetComponent<Gate>();
            if (gate != null)
            {
                int playerKeys = NewPlayer.Instance.keysCollected;
                int playerShards = NewPlayer.Instance.shardsCollected;
                int playerCoins = NewPlayer.Instance.coinsCollected;

                animator.SetBool("gateEntry", true);

                if (gate.CanOpen(playerKeys, playerShards, playerCoins))
                {
                    gate.Open(playerKeys, playerShards, playerCoins);
                }
                else
                {
                    Debug.Log("Not enough keys or shards to open this gate.");
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == NewPlayer.Instance.gameObject)
        { 
            animator.SetBool("gateEntry", false); 
        }
    }
}
