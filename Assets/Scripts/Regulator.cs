using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regulator : MonoBehaviour
{

    [SerializeField]
    float cycleTime = 2.5f; // Time for one half of the cycle

    [SerializeField]
    string playerTag = "Player";

    // Components
    BoxCollider2D regulatorZone;

    float timePassed = 0f;
    [SerializeField]
    float offset = 0.45f;

    void Start()
    {
        regulatorZone = GetComponent<BoxCollider2D>();

        if (regulatorZone != null)
            regulatorZone.isTrigger = true;

        timePassed = 0f;
    }

    void Update()
    {
        timePassed += Time.deltaTime;

        // Check if we should switch states
        if (timePassed > cycleTime)
        {
            timePassed = 0f;
            regulatorZone.offset += new Vector2(0, offset);
            offset = -offset;
        }
    }

    // Only affect the player when they enter the trigger zone
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            HandlePlayerRegulation(other, true);
        }
    }

    // Stop affecting the player when they exit the trigger zone
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            HandlePlayerRegulation(other, false);
        }
    }

    // Handle the actual regulation effect on the player
    void HandlePlayerRegulation(Collider2D playerCollider, bool entering)
    {
        if (entering)
        {
            Debug.Log("Player entered regulation zone");

            Player playerComponent = playerCollider.GetComponent<Player>();
            if (playerComponent != null)
            {
                //regulation effect here
            }
        }
        else
        {
            Debug.Log("Player exited regulation zone");

            Player playerComponent = playerCollider.GetComponent<Player>();
            if (playerComponent != null)
            {
                // regulation effect here
            }
        }
    }
}
