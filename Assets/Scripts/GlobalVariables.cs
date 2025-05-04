using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public static class GlobalVariables
{
    public static bool[,] levels = new bool[8,3]; // change to however many levels there are 

    public static int currentScene = 0;
    public static int clearedLevel = 0;
}
