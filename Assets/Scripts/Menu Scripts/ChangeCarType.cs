using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCarType : MonoBehaviour
{
    int i = 0;
    public Sprite[] spriteList;

    static Sprite finalSprite;

    public SpriteRenderer carSprite;
    // Start is called before the first frame update
    void Start()
    {
        finalSprite = spriteList[0];
        carSprite.sprite = finalSprite;
    }

    public void changeType()
    {
        if (i == spriteList.Length)
        {
            i = 0;
        }
        finalSprite = spriteList[i];
        carSprite.sprite = finalSprite;

        i++;
    }

    public static Sprite getTypeSprite()
    {
        return finalSprite;
    }
}
