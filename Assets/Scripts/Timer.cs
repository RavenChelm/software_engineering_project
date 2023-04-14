using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class Timer
{
    public static DateTime firstRun;
    public static DateTime saveTime;
    public static DateTime gameTime;
    public static void StartTime()
    {
        firstRun = System.DateTime.Now;
    }
    public static void SaveTime()
    {
        saveTime = System.DateTime.Now;
    }
    public static void GameTime()
    {
        // gameTime = saveTime.Subtract(firstRun);
    }

}
