using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regulator : MonoBehaviour
{
    [SerializeField]
    float cycleTime = 5f; // Time for one complete cycle

    [SerializeField]
    float activeTime = 2.5f; // Time the regulator is active within the cycle

    [SerializeField]
    string playerTag = "Player";

    // Components
    BoxCollider2D regulatorZone;

    float timePassed = 0f;
    bool isActive = false;

    void Start()
    {
        regulatorZone = GetComponent<BoxCollider2D>();

        if (regulatorZone != null)
            regulatorZone.isTrigger = true;

        timePassed = 0f;
        DeactivateRegulator();
    }

    void Update()
    {
        timePassed += Time.deltaTime;

        // Check if we should switch states
        if (isActive && timePassed > activeTime)
        {
            DeactivateRegulator();
        }
        else if (!isActive && timePassed > cycleTime - activeTime)
        {
            ActivateRegulator();
        }

        // Reset timer when cycle completes
        if (timePassed > cycleTime)
        {
            timePassed = 0f;
            ActivateRegulator();
        }
    }

    void ActivateRegulator()
    {
        isActive = true;

        // Enable collider for regulation zone
        if (regulatorZone != null)
        {
            regulatorZone.enabled = true;
        }
    }

    void DeactivateRegulator()
    {
        isActive = false;

        // Disable collider for regulation zone
        if (regulatorZone != null)
        {
            regulatorZone.enabled = false;
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
