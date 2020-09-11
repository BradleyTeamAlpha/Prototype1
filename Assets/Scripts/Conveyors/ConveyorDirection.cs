﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorDirection : MonoBehaviour
{
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
