using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextChanger : MonoBehaviour
{
    [SerializeField] private GameObject player;
     private PlayerInterface playerInterface;
    [SerializeField] private Text bombCountText;
    [SerializeField] private Text fireRangeText;

    void Update()
    {
        playerInterface = FindObjectOfType<PlayerInterface>();
        bombCountText.text = "x " + playerInterface.GetBombCount().ToString();
        fireRangeText.text = "x " + playerInterface.GetFiringRange().ToString();
    }
}
