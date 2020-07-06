using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DestructableLootDrop : MonoBehaviour
{
    public int numItemsToDrop;
    private GameObject gameManager;
    public bool keyDrop = false;
    public bool trapDoorDrop = false;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    private void OnDestroy()
    {
        if (keyDrop == true)
        {
            gameManager.GetComponent<LootDrop>().SpawnKey(gameObject);
        }
        else if (trapDoorDrop == true)
        {
            gameManager.GetComponent<LootDrop>().SpawnTrapDoor(gameObject);
        }
        else
        {
            gameManager.GetComponent<LootDrop>().DropLoot(numItemsToDrop, gameObject);
        }
    }
}
