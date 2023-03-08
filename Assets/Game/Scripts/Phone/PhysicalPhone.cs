using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalPhone : MonoBehaviour
{
    [SerializeField] private Light phoneLight;

    private const string NO_PHONE_LIGHT_AVAILABLE = "There is no Light under the Phone for flashlight";

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        //if (phoneLight == null) phoneLight = GetComponentInChildren<Light>();
        //if (phoneLight == null) Debug.LogError(NO_PHONE_LIGHT_AVAILABLE);
    }

    public void EnableLight()
    {
        //phoneLight.gameObject.SetActive(true);
    }

    public void DisableLight()
    {
        //phoneLight.gameObject.SetActive(false);
    }
}
