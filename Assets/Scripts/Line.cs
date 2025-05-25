using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public string playerTag = "Player";
    float timer = 1;

    private void Update()
    {
        timer += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            Player playerComponent = other.GetComponent<Player>();
            if (playerComponent != null && timer >= 1)
            {
                timer = 0;
                playerComponent.lives -= 1;
            }
        }
    }
}
