using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSign : MonoBehaviour
{
    public string playerTag = "Player";

    bool isPunished = false;
    bool hasStopped = false;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            HandlePlayerRegulation(other, true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            HandlePlayerRegulation(other, false);
        }
    }

    void HandlePlayerRegulation(Collider2D playerCollider, bool entering)
    {
        if (entering)
        {
            //Debug.Log("enetered speed limit");

            Player playerComponent = playerCollider.GetComponent<Player>();
            if (playerComponent != null && playerComponent.CheckSpeed() == 0 & hasStopped != true)
            {
                hasStopped = true;
                Debug.Log("stopped");
            }

        }
        else
        {

            Player playerComponent = playerCollider.GetComponent<Player>();
            if (hasStopped != true & isPunished != true)
            {
                isPunished = true;
                playerComponent.lives--;
                Debug.Log("DIDNT STOP");
            }
        }
    }
}
