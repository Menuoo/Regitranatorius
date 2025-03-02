using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

using static Unity.Mathematics.math;

public class Player : MonoBehaviour
{
    [SerializeField, Range(1f, 50f)]
    float acceleration = 20f;
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
        if (Input.GetKey("d"))
        {
            if (speed < speedLimit)
            speed += acceleration * Time.deltaTime * 2f;
        }
        else if (Input.GetKey("a"))
        { 
            if (speed > -speedLimit)
            speed -= acceleration * Time.deltaTime * 2f;
        }
        else if (abs(speed) > 0f)
        {
            speed = speed > 0f ? speed - acceleration * Time.deltaTime : speed + acceleration * Time.deltaTime;
        }

        transform.Translate(new Vector2(speed*Time.deltaTime, 0f));
        wheel1.transform.Rotate(new Vector3(0f, 0f, -speed / 2f));
        wheel2.transform.Rotate(new Vector3(0f, 0f, -speed / 2f));
    }
}
