using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateButton : MonoBehaviour
{
    
    public ConveyorManager manager;

    private bool isRotating = false;

    public float rotateDirection = 90f;
    
    private void Start()
    {
        manager = GameObject.FindWithTag("TileManager").GetComponent<ConveyorManager>();
    }

    private void OnMouseDown()
    {
        if (manager.selectedTiles.Count == 1)
        {
            Rotate(rotateDirection);
            isRotating = true;
        }
    }

    private void Rotate(float degrees)
    {
        manager.selectedTiles[0].transform.Rotate(0, 0, degrees);
    }
}
