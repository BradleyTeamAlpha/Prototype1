using System;
using UnityEngine;

public class ConveyorDirection : MonoBehaviour
{
    [Flags]
    public enum Direction
    {
        noDirection = 0, 
        Left = 1,
        Up = 2,
        Right = 4,
        Down = 8
     }
     public Direction direction;

}
