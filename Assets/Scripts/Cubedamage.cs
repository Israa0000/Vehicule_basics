using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubedamage : MonoBehaviour
{
    [SerializeField] GameObject player; 
    private GameOver gameOverScript;

    private void Start()
    {
        if (gameOverScript == null)
        {
            gameOverScript = FindObjectOfType<GameOver>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(player);
        gameOverScript.TurnPanelOn();
    }
}
