using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLimit : MonoBehaviour
{
    public float limit;
    public TextMesh limitText;
    public string playerTag = "Player";

    bool isPunished = false;


    // Start is called before the first frame update
    void Start()
    {
        limitText.text = limit.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            HandlePlayerRegulation(other, true);
        }
    }
    void OnTriggerStay2D(Collider2D other)
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
            Debug.Log("enetered speed limit");

            Player playerComponent = playerCollider.GetComponent<Player>();
            if (playerComponent != null && playerComponent.speed >limit & isPunished != true)
            {
                isPunished = true;
                playerComponent.lives--;
                Debug.Log("ABOVE SPEED LIMIT");
            }
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
