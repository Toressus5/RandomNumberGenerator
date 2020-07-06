using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    /*PowerUpController powerUpController;
    int randomNumber;
    private void Start()
    {
        powerUpController = GameObject.FindGameObjectWithTag("PowerUpController").GetComponent<PowerUpController>();
        randomNumber = Random.Range(0, powerUpController.powerUps.Count);
    }

    private void OnDestroy()
    {
        powerUpController.SpawnPowerUp(powerUpController.powerUps[randomNumber], gameObject.transform.position);
    }*/

}
