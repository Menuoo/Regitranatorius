using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

using static Unity.Mathematics.math;
using Unity.VisualScripting;
using TMPro;

public class Player : MonoBehaviour
{
    float acceleration = 50f;
    float speed, ySpeed;
    float speedLimit = 30f;
    float originalSpeedLimit = 30f;
    float nitrousSpeedLimit = 50f; // Maximum speed when nitrous is active
    bool isJumping = false;
    bool toggled = false;

    // Nitrous system variables
    float nitrousAmount = 100f; // Total nitrous available
    float nitrousConsumptionRate = 25f; // How fast nitrous depletes
    float nitrousRechargeRate = 5f; // How fast nitrous recharges
    bool isNitrousActive = false;

    // Add a visual indicator for nitrous
    [SerializeField] GameObject nitrousEffect; // Visual effect
    ParticleSystem exhaust;

    [SerializeField]
    GameObject wheel1, wheel2, body, mistake, brakeLight, headLight, speedText, speedArrow, honking;

    BoxCollider2D collider;
    SpriteRenderer redX, brakeCircle, headLightCircle, honkImage;
    TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        collider = gameObject.GetComponent<BoxCollider2D>();
        redX = mistake.GetComponent<SpriteRenderer>();
        honkImage = honking.GetComponent<SpriteRenderer>();
        brakeCircle = brakeLight.GetComponent<SpriteRenderer>();
        headLightCircle = headLight.GetComponent<SpriteRenderer>();
        text = speedText.GetComponent<TMP_Text>();
        exhaust = nitrousEffect.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // Handle nitrous activation
        HandleNitrous();

        // all the basic movements:
        if (Input.GetKey("d")) // move forward
        {
            float accelerationMultiplier = isNitrousActive ? 3f : 2f; // Boost acceleration when nitrous is active
            if (speed < speedLimit)
                speed += acceleration * Time.deltaTime * accelerationMultiplier;
            brakeCircle.enabled = false;
        }
        else if (Input.GetKey("a")) // move backward
        {
            if (speed > -speedLimit)
                speed -= acceleration * Time.deltaTime * 2f;
            brakeCircle.enabled = true;
        }
        else if (abs(speed) > 0f) //automatic deceleration
        {
            if (abs(speed) < 0.1f)
                speed = 0;
            else
                speed = speed > 0f ? speed - acceleration * Time.deltaTime : speed + acceleration * Time.deltaTime;
        }

        if (Input.GetKey("w")) // move up
        {
            if (transform.position.y <= 0f) // limits y position
                transform.Translate(new Vector2(0f, abs(speed * Time.deltaTime / 4f)));
        }
        else if (Input.GetKey("s")) //move down
        {
            if (transform.position.y > -4.5f) // limits y position
                transform.Translate(new Vector2(0f, -abs(speed * Time.deltaTime / 4f)));
        }

        // honk mechanic
        if (Input.GetKeyDown("h"))
        {
            toggled = !toggled;
            honkImage.enabled = toggled;
        }

        // headlight mechanic
        if (Input.GetKeyDown("l")) // headlights
        {
            toggled = !toggled;
            headLightCircle.enabled = toggled;
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

        // If speed exceeds current speed limit (which can happen when nitrous ends), gradually reduce it
        if (abs(speed) > speedLimit)
        {
            float decelerationRate = acceleration * 1.5f * Time.deltaTime;
            speed = speed > 0 ? Mathf.Max(speed - decelerationRate, speedLimit) : Mathf.Min(speed + decelerationRate, -speedLimit);
        }

        transform.Translate(new Vector2(speed * Time.deltaTime, 0f)); // actually makes the car move
        wheel1.transform.Rotate(new Vector3(0f, 0f, -speed / 2f));  // rotates wheel1
        wheel2.transform.Rotate(new Vector3(0f, 0f, -speed / 2f));  // rotates wheel2

        text.SetText(String.Format("{0} km/h", (int)abs(speed)));        // changes display speed
        speedArrow.transform.rotation = Quaternion.Euler(           // rotates speed arrow 
            new Vector3(0f, 0f, abs(speed) * -90f * 0.02f));        // speed can be substituted for  (speed * 50 / speedLimit)
    }

    void HandleNitrous()
    {
        var exhaustColors = exhaust.main;
        // Toggle nitrous on/off with Q key press
        if (Input.GetKeyDown(KeyCode.Q) && nitrousAmount > 0 && !isNitrousActive)
        {
            isNitrousActive = true;
            speedLimit = nitrousSpeedLimit;

            // Enable visual effect if available
            exhaustColors.startColor = new ParticleSystem.MinMaxGradient(Color.white, Color.cyan);
        }
        else if (Input.GetKeyUp(KeyCode.Q) && isNitrousActive)
        {
            DeactivateNitrous();
        }

        // Handle nitrous consumption while active
        if (isNitrousActive)
        {
            nitrousAmount -= nitrousConsumptionRate * Time.deltaTime;


            if (nitrousAmount <= 0)
            {
                nitrousAmount = 0;
                DeactivateNitrous();
            }
        }
        else
        {
            // Recharge nitrous when not in use
            if (nitrousAmount < 100f)
            {
                nitrousAmount += nitrousRechargeRate * Time.deltaTime;
                nitrousAmount = Mathf.Min(nitrousAmount, 100f);
            }
        }
    }

    // Separate function for deactivating nitrous
    void DeactivateNitrous()
    {
        isNitrousActive = false;
        speedLimit = originalSpeedLimit;

        // Disable visual effect if available
        var exhaustColors = exhaust.main;
        exhaustColors.startColor = new ParticleSystem.MinMaxGradient(Color.white, Color.black);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        redX.enabled = true;
        mistake.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
    }

    // function to display nitrous amount on screen
    void OnGUI()
    {
        //GUI.Label(new Rect(10, 10, 200, 20), "Nitrous: " + nitrousAmount.ToString("F0") + "%");
        //GUI.Label(new Rect(10, 30, 200, 20), "Nitrous Active: " + (isNitrousActive ? "YES" : "NO"));
    }
}