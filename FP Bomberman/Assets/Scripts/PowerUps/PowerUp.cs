using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PowerUp
{
    public string name;
    public int powerUpStrenght;

    public GameObject powerUpPrefab;

    public UnityEvent AddPowerUp;

    public void Start()
    {
        if (AddPowerUp != null)
        {
            AddPowerUp.Invoke();
        }
    }
}
