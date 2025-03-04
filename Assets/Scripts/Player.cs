using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

using static Unity.Mathematics.math;

public class Player : MonoBehaviour
{
    [SerializeField, Range(1f, 50f)]
    float acceleration = 50f;
    float speed;
    float speedLimit = 20f;

    [SerializeField]
    GameObject wheel1, wheel2;


    // Start is called before the first frame update
    void Start()
    {
       
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
            if (transform.position.y <= 4.5f) // limits y position
            transform.Translate(new Vector2(0f, abs(speed*Time.deltaTime / 4f)));
        }
        else if (Input.GetKey("s")) //move down
        {
            if (transform.position.y > -4.5f) // limits y position
            transform.Translate(new Vector2(0f, -abs(speed*Time.deltaTime / 4f)));
        }

        // jump mechanic

        if (Input.GetKey("e")) // jump
        {
            if (transform.position.y <= 4.5f) // limits y position
                transform.Translate(new Vector2(0f, abs(speed*2 * Time.deltaTime / 4f)));
        }

        // nitrous mechanic

        if (Input.GetKey("q")) // nitrous
        {
            if(speed < speedLimit)
            speed += acceleration * Time.deltaTime * 2f+25;
        }



        transform.Translate(new Vector2(speed*Time.deltaTime, 0f)); // actually makes the car move
        wheel1.transform.Rotate(new Vector3(0f, 0f, -speed / 2f));  // rotates wheel1
        wheel2.transform.Rotate(new Vector3(0f, 0f, -speed / 2f));  // rotates wheel2
    }
}
