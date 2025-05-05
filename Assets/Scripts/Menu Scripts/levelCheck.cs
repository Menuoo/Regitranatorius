using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelCheck : MonoBehaviour
{
    [SerializeField]
    GameObject passed, time, lives;

    static int levelId = 0;

    // Start is called before the first frame update
    void Start()
    {
        mainManager manager = GetComponent<mainManager>();

        if (manager != null)
            levelId = manager.sceneID;
        else
            levelId = GlobalVariables.clearedLevel;

        accessClearData();
    }


    public static void updateClearData()
    {
        int toUpdate = GlobalVariables.clearedLevel;
        if (true)
            GlobalVariables.levels[toUpdate, 0] = true;

        if (GlobalVariables.clearTime.TotalSeconds < 5)
            GlobalVariables.levels[toUpdate, 1] = true;

        if (true)
            GlobalVariables.levels[toUpdate, 2] = true;
    }

    void accessClearData()
    {
        bool[] data = { 
            GlobalVariables.levels[levelId, 0],
            GlobalVariables.levels[levelId, 1],
            GlobalVariables.levels[levelId, 2] };

        passed.SetActive(data[0]);
        time.SetActive(data[1]);
        lives.SetActive(data[2]);
    }
}
