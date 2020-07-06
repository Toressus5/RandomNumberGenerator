using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpActions : MonoBehaviour
{
    private GameObject player;
    private PlayerInterface playerInterface;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInterface = player.GetComponent<PlayerInterface>();
    }
    public void AddBombCountAction()
    {
        playerInterface.maxNumberOfBombs++;
    }

    public void AddFireRangeAction()
    {
        playerInterface.firingRange++;
    }
}
