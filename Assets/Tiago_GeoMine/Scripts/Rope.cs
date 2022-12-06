using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");   
    }

    void Update()
    {
        transform.position = new Vector2(player.transform.position.x, transform.position.y); // Update X position

        // Change rope length based on how deep the player is
        transform.localScale = new Vector2(transform.localScale.x, Mathf.Abs(player.transform.position.y) * 6.5f);
    }
}
