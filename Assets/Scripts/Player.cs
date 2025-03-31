using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.UI;

using static Unity.Mathematics.math;
using Unity.VisualScripting;
using TMPro;

public class Player : MonoBehaviour
{
    bool alive;
    [SerializeField]
    int lives = 10;
    float timer = 0.3f, timerCnt;

    float acceleration = 50f;
    float speed, ySpeed;
    float speedLimit = 30f;
    float originalSpeedLimit = 30f;
    float nitrousSpeedIncrease = 20f; // Maximum speed when nitrous is active
    bool isJumping = false;
    bool toggled = false;

    // Nitrous system variables
    float nitrousAmount = 100f; // Total nitrous available
    float nitrousConsumptionRate = 40f; // How fast nitrous depletes
    float nitrousRechargeRate = 5f; // How fast nitrous recharges
    bool isNitrousActive = false;

    // Add a visual indicator for nitrous
    [SerializeField] GameObject nitrousEffect; // Visual effect
    ParticleSystem exhaust;

    [SerializeField]
    GameObject wheel1, wheel2, body, brakeLight, headLight, speedText, gearText, livesObj, speedArrow, honking, nitrousSlider;

    BoxCollider2D carCollider;
    SpriteRenderer redX, brakeCircle, headLightCircle, honkImage, bodySprite;
    TMP_Text text, gearChangeText, livesText;
    Slider nitrousBar;

    Dictionary<string, KeyCode> keyboard;



    // Start is called before the first frame update
    void Start()
    {
        bodySprite = body.GetComponent<SpriteRenderer>();
        carCollider = gameObject.GetComponent<BoxCollider2D>();
        honkImage = honking.GetComponent<SpriteRenderer>();
        brakeCircle = brakeLight.GetComponent<SpriteRenderer>();
        headLightCircle = headLight.GetComponent<SpriteRenderer>();
        text = speedText.GetComponent<TMP_Text>();
        gearChangeText= gearText.GetComponent<TMP_Text>();
        exhaust = nitrousEffect.GetComponent<ParticleSystem>();
        nitrousBar = nitrousSlider.GetComponent<Slider>();
        livesText = livesObj.GetComponent<TMP_Text>();

        alive = true;
        bodySprite.color = CarColour.getColour();

        keyboard = controlsManager.controls;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive) 
            PlayerControls();

        if (lives <= 0)
        {
            Die();
            if (abs(speed) < 0.1f)
                speed = 0;
            else
                speed = speed > 0f ? speed - acceleration / 2 * Time.deltaTime : speed + acceleration / 2 * Time.deltaTime;
        }

        nitrousBar.value = nitrousAmount; // slider
        livesText.SetText(String.Format("x{0}", max(0, lives)));

        if (isJumping)
        {
            ySpeed -= acceleration * Time.deltaTime;
            body.transform.Translate(new Vector2(0f, ySpeed * Time.deltaTime));

            if (body.transform.localPosition.y <= 0f)
            {
                body.transform.localPosition = new Vector3(0f, 0f, 0f);
                isJumping = false;
                carCollider.enabled = true;
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

    void PlayerControls() 
    {
        // Handle nitrous activation
        HandleNitrous();

        // all the basic movements:
        if (Input.GetKey(keyboard["forward"])) // move forward
        {
            float accelerationMultiplier = isNitrousActive ? 3f : 2f; // Boost acceleration when nitrous is active
            if (speed < speedLimit)
                speed += acceleration * Time.deltaTime * accelerationMultiplier;
            brakeCircle.enabled = false;
        }
        else if (Input.GetKey(keyboard["backward"])) // move backward
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

        if (Input.GetKey(keyboard["upward"])) // move up
        {
            if (transform.position.y <= 0f) // limits y position
                transform.Translate(new Vector2(0f, abs(speed * Time.deltaTime / 4f)));
        }
        else if (Input.GetKey(keyboard["downward"])) //move down
        {
            if (transform.position.y > -4.5f) // limits y position
                transform.Translate(new Vector2(0f, -abs(speed * Time.deltaTime / 4f)));

        }

        // honk mechanic
        if (Input.GetKeyDown(keyboard["honk"]))
        {
            honkImage.enabled = honkImage.enabled ? false : true;
        }

        // headlight mechanic
        if (Input.GetKeyDown(keyboard["headlight"])) // headlights
        {
            headLightCircle.enabled = headLightCircle.enabled ? false : true;
        }

        // jump mechanic
        if (Input.GetKeyDown(keyboard["jump"]) && !isJumping) // jump
        {
            isJumping = true;
            carCollider.enabled = false;
            ySpeed = 20f; // set the jumping speed (and height)
        }

        // parking gear toggle
        if (Input.GetKeyDown(keyboard["gear"])) 
        {
            toggled = !toggled;
            if (toggled)
            {
                gearChangeText.SetText(String.Format("P"));
                speedLimit = 10;
                originalSpeedLimit = 10;
            }
            else
            {
                gearChangeText.SetText(String.Format("D"));
                speedLimit = 30;
                originalSpeedLimit = 30;
            }

            // labai brokuotas budas nusistatyt greiti tiesiog tarp 30 ir 10 keitinejas
            // original irgi keicia nes nitrous taip veikia kad sugrizta ne i buvusi o nustatyta
        }


    }

    void HandleNitrous()
    {
        var exhaustColors = exhaust.main;
        // Toggle nitrous on/off with Q key press
        if (Input.GetKeyDown(keyboard["nitrous"]) && nitrousAmount > 0 && !isNitrousActive)
        {
            isNitrousActive = true;
            speedLimit = speedLimit + nitrousSpeedIncrease;

            // Enable visual effect if available
            exhaustColors.startColor = new ParticleSystem.MinMaxGradient(Color.white, Color.cyan);
        }
        else if (Input.GetKeyUp(keyboard["nitrous"]) && isNitrousActive)
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
        lives--;
    }

    void Die()
    {
        alive = false;
        bodySprite.color = new Color(0.1f, 0.1f, 0.1f, 1f);
        body.transform.localScale = new Vector3(1f, -1f , 1f);
        wheel1.SetActive(false); // might delete this idk

        exhaust.Stop();

        timerCnt += Time.deltaTime;
        if (timerCnt >= timer)
        {
            brakeCircle.enabled = brakeCircle.enabled ? false : true;
            timerCnt = 0;
        }
    }
}