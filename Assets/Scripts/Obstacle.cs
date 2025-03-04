using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    [SerializeField]
    float x_hitbox, y_hitbox;
    [SerializeField]
    Sprite sprite;

    

    // Start is called before the first frame update
    void Start()
    {
       
        spriteRenderer.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
