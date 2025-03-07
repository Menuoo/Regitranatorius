using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

using static Unity.Mathematics.math;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    float acceleration = 50f;
    float speed, ySpeed;
    float speedLimit = 30f;
    bool isJumping = false;

    [SerializeField]
    GameObject wheel1, wheel2, body, mistake;

    BoxCollider2D collider;
    SpriteRenderer redX;

    // Start is called before the first frame update
    void Start()
    {
        collider = gameObject.GetComponent<BoxCollider2D>();
        redX = mistake.GetComponent<SpriteRenderer>();
    }



    // Update is called once per frame
    void Update()
    {
        // all the basic movements:

        if (Input.GetKey("d")) // move forward
        {
            if (speed < speedLimit)
            speed += acceleration * Time.deltaTime * 2f;
        }
        else if (Input.GetKey("a")) // move backward
        { 
            if (speed > -speedLimit)
            speed -= acceleration * Time.deltaTime * 2f;
        }
        else if (abs(speed) > 0f) //automatic decceleration
        {
            speed = speed > 0f ? speed - acceleration * Time.deltaTime : speed + acceleration * Time.deltaTime;
        }

        if (Input.GetKey("w")) // move up
        {
            if (transform.position.y <= 0f) // limits y position
            transform.Translate(new Vector2(0f, abs(speed*Time.deltaTime / 4f)));
        }
        else if (Input.GetKey("s")) //move down
        {
            if (transform.position.y > -4.5f) // limits y position
            transform.Translate(new Vector2(0f, -abs(speed*Time.deltaTime / 4f)));
        }


        // jump mechanic
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping) // jump
        {
            isJumping = true;
            collider.enabled = false;
            ySpeed = 20f; // set the jumping speed (and height)
        }
        if (isJumping)
        {
            ySpeed -= acceleration * Time.deltaTime;
            body.transform.Translate(new Vector2(0f, ySpeed * Time.deltaTime));

            if (body.transform.localPosition.y <= 0f)
            {
                body.transform.localPosition = new Vector3(0f, 0f, 0f);
                isJumping = false;
                collider.enabled = true;
            }
        }
        // end of jump mechanic



        // nitrous mechanic

        /*if (Input.GetKey("q")) // nitrous
        {
            if(speed < speedLimit)
            speed += acceleration * Time.deltaTime * 2f+25;
        }*/


        transform.Translate(new Vector2(speed*Time.deltaTime, 0f)); // actually makes the car move
        wheel1.transform.Rotate(new Vector3(0f, 0f, -speed / 2f));  // rotates wheel1
        wheel2.transform.Rotate(new Vector3(0f, 0f, -speed / 2f));  // rotates wheel2
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("something");
        redX.enabled = true;
        mistake.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
    }
}
