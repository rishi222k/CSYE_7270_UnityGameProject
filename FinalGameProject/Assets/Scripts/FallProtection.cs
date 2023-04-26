using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallProtection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //If I touch an enemy, hurt the enemy! 
        if (collision.gameObject == NewPlayer.Instance.gameObject)
        {
            NewPlayer.Instance.health=0;
        }
    }
}
