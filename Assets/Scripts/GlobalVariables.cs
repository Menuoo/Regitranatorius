using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using System.Diagnostics;
using System;

public static class GlobalVariables
{
    public static bool[,] levels = new bool[8,3]; // change to however many levels there are 

    public static Stopwatch sw = new Stopwatch();
    public static TimeSpan clearTime = new TimeSpan();

    public static int currentScene = 0;
    public static int clearedLevel = 0;

    public static bool showResults = false; 

    public static void ResetTimer()
    { 
        sw.Reset();
        sw.Start();
    }
    public static Stopwatch StopTimer()
    { 
        sw.Stop();
        return sw;
    }
}
