using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    enum ItemType { Coin, Health, Key, Shard } //Creates an ItemType enum (drop down)
    [SerializeField] private ItemType itemType;
    NewPlayer newPlayer;

    // Start is called before the first frame update
    void Start()
    {
        newPlayer = GameObject.Find("Player").GetComponent<NewPlayer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player is touching me, print "Collect" in the console
        if (collision.gameObject.name == "Player")
        {
            switch (itemType)
            {
                case ItemType.Coin:
                    newPlayer.coinsCollected += 1;
                    break;
                case ItemType.Health:
                    newPlayer.health = Mathf.Min(newPlayer.health + 20, 100);
                    break;
                case ItemType.Key:
                    newPlayer.keysCollected += 1;
                    break;
                case ItemType.Shard:
                    newPlayer.shardsCollected += 1;
                    break;
            }

            newPlayer.UpdateUI();
            Destroy(gameObject);
        }
    }


}



