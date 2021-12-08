using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundData : MonoBehaviour
{
    public static void SetAudio(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public static int GetAudio(string key)
    {
       return PlayerPrefs.GetInt(key);
    }
}
