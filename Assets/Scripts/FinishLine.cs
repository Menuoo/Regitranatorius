using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public string playerTag = "Player";

    void Activate()
    {
        GlobalVariables.clearedLevel = GlobalVariables.currentScene;
        mainManager.ChangeScene(3);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            Activate();
        }
    }
}
