using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool newSave=true;
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
        // If the player is touching me, check if the gate can be opened and open if possible
        if (collision.gameObject == NewPlayer.Instance.gameObject)
        {
            animator.SetBool("save", true);
            if(NewPlayer.Instance.RescueOps > 0 && newSave)
            {
                NewPlayer.Instance.RescueOps -= 1;
                NewPlayer.Instance.UpdateUI();
            }
            newSave = false;
        }
        
    }
}
