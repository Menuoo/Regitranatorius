using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stoplight : MonoBehaviour
{
    [SerializeField]
    float update_time;

    [SerializeField]
    GameObject light;

    BoxCollider2D collider;
    SpriteRenderer lightCircle;
    
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        lightCircle = light.GetComponent<SpriteRenderer>();
        timePassed = update_time;
    }


    float timePassed = 0f;
    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > update_time * 2)
        {
            lightCircle.color = new Color(0.9f, 0f, 0f, 0.4f);
            light.transform.localPosition = new Vector3(0f, 0.1f, 0f);
            collider.enabled = true;
            timePassed = 0f;
        }
        else if (timePassed>update_time)
        {
            lightCircle.color = new Color(0f, 0.9f, 0f, 0.4f);
            light.transform.localPosition = Vector3.zero;
            collider.enabled = false;
        }
    }
}
