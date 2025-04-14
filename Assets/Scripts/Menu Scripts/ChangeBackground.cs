using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackground : MonoBehaviour
{
    int i = 0;
    public Sprite[] spriteList;
   
    static Sprite finalSprite;

    public SpriteRenderer background;
    // Start is called before the first frame update
    void Start()
    {
        finalSprite = spriteList[0];
        background.sprite = finalSprite;
    }

    public void changeBackground()
    {
        if(i==spriteList.Length)
        {
            i = 0;
        }
        finalSprite = spriteList[i];
        background.sprite = finalSprite;

        i++;
    }

    public static Sprite getBackgroundSprite()
    {
        return finalSprite;
    }
}
