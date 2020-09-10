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
    int totalBits = 32;
    uint rotateLeft(uint x, int y)
    {
        uint k = (x << y) | (x >> (totalBits - y));
        if( k > 8)
        {
            k = 1;
        }
        return k;
    }
    uint rotateRight(uint x, int y)
    {
        uint k = (x >> y) | (x << (totalBits - y));
        if(k == 2147483648)
        {
            k = 8;
        }
        return k;
    }
    private void Rotate(float degrees)
    {
       
        manager.selectedTiles[0].transform.Rotate(0, 0, degrees);

        /*
        if(degrees > 0)
        {
            Microsoft.VisualStudio.Utilities.RotateRight(manager.selectedTiles[0].GetComponent<ConveyorDirection>().shifter, 1);
        }
        else
        {
            Microsoft.VisualStudio.Utilities.RotateLeft(manager.selectedTiles[0].GetComponent<ConveyorDirection>().shifter, 1);
        }
       */
        ConveyorDirection shorten = manager.selectedTiles[0].GetComponent<ConveyorDirection>();
        //Debug.Log("Hopefully shifting");
        if(degrees > 0)
        {
            //Debug.Log("shifting right");
            uint right = rotateRight((uint)shorten.shifter, 1);
            Debug.Log("Right value is" + right);
            shorten.shifter = (ConveyorDirection.bitShiftMayhem)right;
        }
        else
        {
            Debug.Log("shifting left");
            uint left =  rotateLeft((uint)shorten.shifter, 1);
            shorten.shifter = (ConveyorDirection.bitShiftMayhem)left;
        }
       
        
    }
}
