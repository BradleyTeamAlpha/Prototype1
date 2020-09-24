using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConveyorManager : MonoBehaviour
{
    
    [Tooltip("Tiles the player has currently selected")]
    public List<GameObject> selectedTiles;

    [Tooltip("What color selected tiles should be")]
    public Color selectedColor = new Color(0, 0.5f, 0);

    [Tooltip("How many swaps the player has done")]
    public int swapsDone = 0;

    [Tooltip("Maximum number of swaps")]
    public int totalSwaps = Int32.MaxValue;
    
    [Tooltip("Is the puzzle currently running (is coal being spawned)")]
    public bool isRunning = false;

    public Text swapsDoneText;
    /*
     * if list is length 2
     *     swap tiles
     *
     * if list is length 1 and button is hit
     *     do thing (rotate, undo etc...)
     */

    private void Update()
    {
        swapsDoneText.text = swapsDone.ToString();
        foreach (var tile in selectedTiles)
        {
            SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();
            sr.color = selectedColor;
        }
        
        // Make sure 2 tiles are selected
        if (selectedTiles.Count == 2)
        {
            // Make sure the selected tiles are not the same, and that the player has swaps left
            if (!selectedTiles[0].Equals(selectedTiles[1]) && swapsDone < totalSwaps)
            {
                // Make sure the selected tiles share a color
                if ((selectedTiles[0].GetComponent<ConveyorSelector>().color &
                    selectedTiles[1].GetComponent<ConveyorSelector>().color) != ConveyorSelector.TileColor.COLOR_NONE)
                {
                    Vector3 temp = selectedTiles[0].transform.position;
                    selectedTiles[0].transform.position = selectedTiles[1].transform.position;
                    selectedTiles[1].transform.position = temp;
                    selectedTiles[1].GetComponent<SpriteRenderer>().color =
                        selectedTiles[1].GetComponent<ConveyorSelector>().startColor;
                    ++swapsDone;
                }
            }

            selectedTiles[0].GetComponent<SpriteRenderer>().color = selectedTiles[0].GetComponent<ConveyorSelector>().startColor;

            selectedTiles.Clear();
        }
    }
    
    /// <summary>
    /// Rotates the given number x by y spaces
    /// </summary>
    /// <param name="x">The number to rotate</param>
    /// <param name="y">How many spaces to rotate</param>
    /// <returns>An 8-bit rotated number</returns>
    byte RotateLeft(byte x, byte y, bool wrap = true)
    {
        //(original << bits) | (original >> (32 - bits))
        byte k = (byte)(x << y);
        if(wrap & k > 8)
        {
            k = 1;
        }
        return k;
    }
    
    /// <summary>
    /// Rotates the given number x by y spaces
    /// </summary>
    /// <param name="x">The number to rotate</param>
    /// <param name="y">How many spaces to rotate</param>
    /// <returns>An 8-bit rotated number</returns>
    byte RotateRight(byte x, byte y, bool wrap = true)
    {
        //(original >> bits) | (original << (32 - bits))
        byte k = (byte)(x >> y);
        if(wrap & k < 1)
        {
            k = 8;
        }
        return k;
    }
    public void RotateTile(float degrees)
    {
        if (!selectedTiles[0].GetComponent<ConveyorSelector>().canRotate)
        {
            return;
        }
       
        selectedTiles[0].transform.Rotate(0, 0, degrees);

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

        ConveyorDirection shorten = selectedTiles[0].GetComponent<ConveyorDirection>();

        MultiExitTiles multiExitTiles = selectedTiles[0].GetComponent<MultiExitTiles>();
        
        if(degrees > 0)
        {
            uint right = RotateRight((byte) shorten.direction, 1);
            shorten.direction = (ConveyorDirection.Direction) right;
            if (multiExitTiles != null)
            {
                bool isOdd = (multiExitTiles.outputDirections & ConveyorDirection.Direction.Left) != 0;
                byte temp = RotateRight((byte)multiExitTiles.outputDirections, 1, false);
                Debug.Log("Temp: " + temp);
                if (isOdd)
                {
                    temp += 8;
                }
                multiExitTiles.outputDirections = (ConveyorDirection.Direction)temp;
            }
        }
        else
        {
            uint left = RotateLeft((byte) shorten.direction, 1);
            shorten.direction = (ConveyorDirection.Direction) left;
            if (multiExitTiles != null)
            {
                byte temp = RotateLeft((byte)multiExitTiles.outputDirections, 1, false);
                bool didMax = false;
                Debug.Log("Temp: " + temp);
                if (temp > 15)
                {
                    temp -= 15;
                    didMax = true;
                }
                multiExitTiles.outputDirections = (ConveyorDirection.Direction)temp;
                if (didMax)
                {
                    multiExitTiles.outputDirections |= ConveyorDirection.Direction.Left;
                }
            }
        }

    }

    public void ToggleSpawning()
    {
        isRunning = !isRunning;
    }
    
}
