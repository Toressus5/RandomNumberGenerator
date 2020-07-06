using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRotation : MonoBehaviour
{
    public int spinX;
    public int spinY;
    public int spinZ;

    void Update()
    {
        Transform dropTransform = gameObject.transform;
        dropTransform.Rotate(spinX, spinY, spinZ);
    }
}
