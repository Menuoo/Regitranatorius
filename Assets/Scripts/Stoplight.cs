using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    [SerializeField]
    float update_time;
    [SerializeField]
    Sprite sprite;


    
    // Start is called before the first frame update
    void Start()
    {
       
        spriteRenderer.sprite = sprite;
        spriteRenderer.color = Color.red;
    }


    float timePassed = 0f;
    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if(timePassed>update_time)
        {
            spriteRenderer.color = Color.green;
        }
        if(timePassed>update_time*2)
        {
            spriteRenderer.color = Color.red;
            timePassed = 0f;
        }
    }
}
