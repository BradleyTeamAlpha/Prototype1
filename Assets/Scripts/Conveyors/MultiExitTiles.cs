using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConveyorDirection))]
public class MultiExitTiles : MonoBehaviour
{

    public ConveyorDirection.Direction outputDirections;

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
        // Assume rotating the currentDirection var
        // & it with output directions so it picks one it is allowed to output to
        //(original >> bits) | (original << (32 - bits))
        uint k = (x >> y);
        ConveyorDirection.Direction newDir =  ((ConveyorDirection.Direction)k & outputDirections);
        while (newDir == ConveyorDirection.Direction.noDirection)
        {
            k = k >> 1;
            if (k < 1)
            {
                k = 8;
            }
            newDir =  ((ConveyorDirection.Direction)k & outputDirections);
        }
        return k;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        uint x = (uint)select.direction;
        //x = RotateLeft(x, 2);
        x = RotateRight(x, 1);
        select.direction = (ConveyorDirection.Direction)x;
    }
}
