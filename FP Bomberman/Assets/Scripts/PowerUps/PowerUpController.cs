using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public GameObject powerUpPrefab;

    public List<PowerUp> powerUps;

    public Dictionary<PowerUp, float> activePowerUps = new Dictionary<PowerUp, float>();

    private List<PowerUp> keys = new List<PowerUp>();

    private void Update()
    {
        HandleActivePowerUps();
    }

    public void HandleActivePowerUps()
    {
        bool changed = false;

        if (activePowerUps.Count > 0)
        {
            foreach (PowerUp powerUp in keys)
            {
                if (activePowerUps[powerUp] > 0)
                {
                    activePowerUps[powerUp] -= Time.deltaTime;
                }
                else
                {
                    changed = true;

                    activePowerUps.Remove(powerUp);

                }
            }
        }

        if (changed)
        {
            keys = new List<PowerUp>(activePowerUps.Keys);
        }
    }
    
    public void ActivatePowerUp(PowerUp powerUp)
    {
        if (!activePowerUps.ContainsKey(powerUp))
        {
            powerUp.Start();
            activePowerUps.Add(powerUp, powerUp.powerUpStrenght);
        }
        else
        {
            activePowerUps[powerUp] += powerUp.powerUpStrenght;
        }

        keys = new List<PowerUp>(activePowerUps.Keys);
    }

    public GameObject SpawnPowerUp(PowerUp powerUp, Vector3 position)
    {
        GameObject powerUpGameObject = Instantiate(powerUp.powerUpPrefab);

        var powerUpBehaviour = powerUpGameObject.GetComponent<PowerUpBehaviour>();

        powerUpBehaviour.controller = this;

        powerUpBehaviour.SetPowerUp(powerUp);

        powerUpGameObject.transform.position = position;

        return powerUpGameObject;
    }
}
