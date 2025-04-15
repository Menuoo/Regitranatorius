using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stoplight : MonoBehaviour
{
    [SerializeField]
    float update_time;

    [SerializeField]
    GameObject lightStop;

    BoxCollider2D lightCollider;
    SpriteRenderer lightCircle;

    public string playerTag = "Player";

    //Dictionary<string, string[]> name = new Dictionary<string, string[]>();

    // Start is called before the first frame update
    void Start()
    {
        lightCollider = GetComponent<BoxCollider2D>();
        lightCircle = lightStop.GetComponent<SpriteRenderer>();
        timePassed = update_time;

        //name.Add("pav", new string[] { "as", "tu" });
        //print(name["pav"][0]);
    }


    float timePassed = 0f;
    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > update_time * 2)
        {
            lightCircle.color = new Color(0.9f, 0f, 0f, 0.4f);
            lightStop.transform.localPosition = new Vector3(0f, 0.1f, 0f);
            lightCollider.enabled = true;
            timePassed = 0f;
        }
        else if (timePassed>update_time)
        {
            lightCircle.color = new Color(0f, 0.9f, 0f, 0.4f);
            lightStop.transform.localPosition = Vector3.zero;
            lightCollider.enabled = false;
        }
    }

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
        if (entering)
        {
            Player playerComponent = playerCollider.GetComponent<Player>();
            
                playerComponent.lives--;
                Debug.Log("REDLIGHT");
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
