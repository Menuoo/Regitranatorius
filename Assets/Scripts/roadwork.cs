using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roadwork : MonoBehaviour
{
    public string playerTag = "Player";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            HandlePlayerRegulation(other, true);
        }
    }
    void HandlePlayerRegulation(Collider2D playerCollider, bool entering)
    {
        if (entering)
        {
            Debug.Log("enetered speed limit");

            Player playerComponent = playerCollider.GetComponent<Player>();
            playerComponent.speed = 0;
            playerComponent.lives--;
        }
        else
        {

            Player playerComponent = playerCollider.GetComponent<Player>();
            if (playerComponent != null)
            {
                // regulation effect here
            }
        }
    }
}
