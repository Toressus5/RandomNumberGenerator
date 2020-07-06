using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    public PowerUpController controller; 

    [SerializeField]
    private PowerUp powerUp;

    private Transform transform_;

    void Awake()
    {
        transform_ = transform;
    }

    private void ActivatePowerUp()
    {
        controller.ActivatePowerUp(powerUp);
    }

    public void SetPowerUp(PowerUp powerUp)
    {
        this.powerUp = powerUp;
        gameObject.name = powerUp.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ActivatePowerUp();
            gameObject.SetActive(false);
        }
    }
}
