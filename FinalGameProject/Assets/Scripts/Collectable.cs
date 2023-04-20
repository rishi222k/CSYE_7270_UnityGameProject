using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    enum ItemType { Coin, Health, Key, Shard } //Creates an ItemType enum (drop down)
    [SerializeField] private ItemType itemType;

    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player is touching me, print "Collect" in the console
        if (collision.gameObject == NewPlayer.Instance.gameObject)
        {
            switch (itemType)
            {
                case ItemType.Coin:
                    NewPlayer.Instance.coinsCollected += 1;
                    break;
                case ItemType.Health:
                    NewPlayer.Instance.health = Mathf.Min(NewPlayer.Instance.health + 20, 100);
                    break;
                case ItemType.Key:
                    NewPlayer.Instance.keysCollected += 1;
                    break;
                case ItemType.Shard:
                    NewPlayer.Instance.shardsCollected += 1;
                    break;
            }

            NewPlayer.Instance.UpdateUI();
            Destroy(gameObject);
        }
    }


}



