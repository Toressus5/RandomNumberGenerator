using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDrop : MonoBehaviour
{
    public GenericLootDropTableGameObject lootDropTable;

    [SerializeField]
    private GameObject keyPrefab;
    [SerializeField]
    private GameObject trapDoorPrefab;

    PowerUpController powerUpController;

    private void Start()
    {
        powerUpController = GameObject.FindGameObjectWithTag("PowerUpController").GetComponent<PowerUpController>();
    }
    void OnValidate()
    {
        lootDropTable.ValidateTable();
    }

    public void DropLoot(int numItemsToDrop, GameObject destructibleWall)
    {
        for (int i = 0; i < numItemsToDrop; i++)
        {
            GenericLootDropItemGameObject selectedItem = lootDropTable.PickLootDropItem();
            GameObject selectedItemGameObject = Instantiate(selectedItem.item);
            DeterminePowerUp(selectedItemGameObject);
            selectedItemGameObject.transform.position = destructibleWall.transform.position;
        }
    }

    public void SpawnKey(GameObject destructibleWall)
    {
        GameObject key = Instantiate(keyPrefab, destructibleWall.transform.position, Quaternion.identity);
    }

    public void SpawnTrapDoor(GameObject destructibleWall)
    {
        GameObject trapDoor = Instantiate(trapDoorPrefab, destructibleWall.transform.position, Quaternion.identity);
    }

    private void DeterminePowerUp(GameObject selectedItem)
    {
        foreach (PowerUp powerUp in powerUpController.powerUps)
        {
            Debug.Log(powerUp.name);
            Debug.Log(selectedItem.name);
            if (selectedItem.name == powerUp.powerUpPrefab.name)
            {
                Debug.Log("here");
                powerUpController.SpawnPowerUp(powerUp, gameObject.transform.position);
            }
        }
    }
}
