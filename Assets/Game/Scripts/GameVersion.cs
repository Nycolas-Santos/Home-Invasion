using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVersion : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(50,50,200,50),"DEVELOPMENT MODE");
        GUI.Label(new Rect(50,75,200,50),"Game Version: " + Application.version + " - " + Application.unityVersion);
    }
}
