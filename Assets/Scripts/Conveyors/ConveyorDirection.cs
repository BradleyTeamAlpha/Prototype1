using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorDirection : MonoBehaviour
{
   // [Flags]
  public enum bitShiftMayhem
    {
        noDirection = 0, 
        Left = 1,
        Up = 2,
        Right = 4,
        Down = 8
    }
    public bitShiftMayhem shifter;

}
