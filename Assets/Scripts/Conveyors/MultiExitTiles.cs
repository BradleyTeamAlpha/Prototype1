using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiExitTiles : MonoBehaviour
{
   private ConveyorDirection select;
   void Start()
    {
        select = gameObject.GetComponent<ConveyorDirection>();//cache the ConveyorDirection

    }
    uint RotateLeft(uint x, int y)
    {
        //(original << bits) | (original >> (32 - bits))
        uint k = (x << y);
        if (k > 8)
        {
            k = 1;
        }
        if(x == 8)
        {
            k = 2;
        }
        return k;
    }
    uint RotateRight(uint x, int y)
    {
        //(original >> bits) | (original << (32 - bits))
        uint k = (x >> y);
        if (k < 1)
        {
            k = 8;
        }
        return k;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        uint x = (uint)select.direction;
        x = RotateLeft(x, 2);
        select.direction = (ConveyorDirection.Direction)x;
        Debug.Log("This is being called");
    }
}
