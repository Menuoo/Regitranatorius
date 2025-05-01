using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public string playerTag = "Player";
    bool isPunished = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            HandlePlayerRegulation(other, true);
        }
    }

    // Handle the actual regulation effect on the player
    void HandlePlayerRegulation(Collider2D playerCollider, bool entering)
    {
        if (entering && !isPunished)
        {
            Player playerComponent = playerCollider.GetComponent<Player>();

            playerComponent.lives++;
            isPunished = true;
            Debug.Log("HEALTH PICKUP");
            gameObject.SetActive(false);
        }
        else
        {

            Player playerComponent = playerCollider.GetComponent<Player>();
            if (playerComponent != null)
            {
            }
        }
    }
}
