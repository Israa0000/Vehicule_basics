using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.Playables;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Transform[] spawnposition;
    Player Player;
    int randomIndex;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            randomIndex = Random.Range(0, spawnposition.Length);
            Transform newSpawnPoint = spawnposition[randomIndex];

            transform.position = newSpawnPoint.position;
        }
    }

}
