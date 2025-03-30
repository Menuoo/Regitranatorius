using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarColour : MonoBehaviour
{
    static Color carColour = Color.white;
    int clr = 8;

    [SerializeField]
    GameObject car;

    SpriteRenderer carSprite;

    private void Start()
    {
        carSprite = GetComponent<SpriteRenderer>();
        changeColour();
    }

    

    public void changeColour()
    {
        if (clr > 8)
            clr = 0;

        //yellow, green, red, blue, white, cyan, gray, purple, orange

        switch (clr)
        {
            case 0: { carColour = Color.yellow; break; }
            case 1: { carColour = Color.green; break; }
            case 2: { carColour = Color.red; break; }
            case 3: { carColour = Color.blue; break; }
            case 4: { carColour = Color.magenta; break; }
            case 5: { carColour = Color.cyan; break; }
            case 6: { carColour = Color.gray; break; }
            case 7: { carColour = new Color(1, 0.5f, 0); break; }
            case 8: { carColour = Color.white; break; }
            default: break;
        }

        carSprite.color = carColour;

        clr++;
    }

    public static Color getColour()
    { return carColour; }
}
