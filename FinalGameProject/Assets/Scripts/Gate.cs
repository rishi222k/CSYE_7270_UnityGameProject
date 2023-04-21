using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private int requiredKeys;
    [SerializeField] private int requiredShards;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CanOpen(int playerKeys, int playerShards)
    {
        return playerKeys >= requiredKeys && playerShards >= requiredShards;
    }

    public void Open(int playerKeys, int playerShards)
    {
        if (CanOpen(playerKeys, playerShards))
        {
            // Deduct the required keys and shards from the player's inventory
            NewPlayer.Instance.keysCollected -= requiredKeys;
            NewPlayer.Instance.shardsCollected -= requiredShards;

            // Open the gate, perform your desired action here, e.g., animation or sound
            Debug.Log("Gate opened!");
            NewPlayer.Instance.UpdateUI();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Not enough keys or shards to open the gate.");
        }
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

                if (gate.CanOpen(playerKeys, playerShards))
                {
                    gate.Open(playerKeys, playerShards);
                }
                else
                {
                    Debug.Log("Not enough keys or shards to open this gate.");
                }
            }
        }
    }
}
