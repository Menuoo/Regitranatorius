using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setBackground : MonoBehaviour
{
    public SpriteRenderer currentBG;
    // Start is called before the first frame update
    void Start()
    {
        currentBG.sprite = ChangeBackground.getBackgroundSprite();
    }
}
