using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainManager : MonoBehaviour
{
    [SerializeField]
    public int sceneID;

    public void StartGame() 
    {
        GlobalVariables.ResetTimer();
        GlobalVariables.currentScene = sceneID;
        SceneManager.LoadScene(sceneID);
    }

    public void Settings()
    { 
        //settings button code
    }

    public void CloseGame()
    { 
        Application.Quit();
    }

    public static void ChangeScene(int scene)
    {
        mainManager manager = new mainManager();
        manager.sceneID = scene;
        manager.StartGame();
    }
}
