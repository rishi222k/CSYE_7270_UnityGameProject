using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLaodTrigger : MonoBehaviour
{
    [SerializeField] private string loadSceneString;
    [SerializeField] private bool destroyPlayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == NewPlayer.Instance.gameObject)
        {
            SceneManager.LoadScene(loadSceneString);
            if (destroyPlayer)
            {
                Destroy(NewPlayer.Instance.gameObject);
                Destroy(GameManager.Instance.gameObject);
            }
        }
    }
}


