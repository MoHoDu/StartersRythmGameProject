using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    public static string ID = "aaa";
    public static string PASS = "12345";
    public static int charNum = 0;
    public static int charMaxNum = 1;
    public static string nick = " ";

    public static bool isGame = false;
    public static float hp = 1;

    public static bool[] houseLock = { true, true, true };

    public static List<AudioClip> notes = new List<AudioClip>();
}
