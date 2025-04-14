using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaking : MonoBehaviour
{
    public bool shakeCamera;
    public bool shakeObject;
    public float shakeAmount;
    void Update ()
    {
        if(shakeCamera)
        {
            // https://www.youtube.com/watch?v=SOxutBMCOUc
        }
        if (shakeObject)
        {
            Vector2 offset = Random.insideUnitCircle * (Time.deltaTime * shakeAmount);
            transform.position = new Vector2(transform.position.x, transform.position.y + offset.y);

        }
    }
}
