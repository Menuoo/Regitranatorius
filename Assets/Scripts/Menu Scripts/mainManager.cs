using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainManager : MonoBehaviour
{
    [SerializeField]
    int sceneID;

    public void StartGame() 
    {
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

}
