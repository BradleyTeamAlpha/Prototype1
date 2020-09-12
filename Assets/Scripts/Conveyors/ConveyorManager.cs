﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    [Tooltip("Can only adjcent swaps be made")]
    public bool isAdjcency = false;
    /*
     * if list is length 2
     *     swap tiles
     *
     * if list is length 1 and button is hit
     *     do thing (rotate, undo etc...)
     */

    private void Update()
    {
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
                    // Calculate the difference in their positions
                    ConveyorDirection dir0 = selectedTiles[0].GetComponent<ConveyorDirection>();
                    ConveyorDirection dir1 = selectedTiles[1].GetComponent<ConveyorDirection>();
                    float xDir = Mathf.Abs((selectedTiles[0].transform.position.x + 8) -
                                           (selectedTiles[1].transform.position.x + 8));
                    float yDir = Mathf.Abs((selectedTiles[0].transform.position.y + 8) -
                                           (selectedTiles[1].transform.position.y + 8));
                    
                    if (!isAdjcency || (xDir <= 2 && yDir <= 2))
                    {
                        Vector3 temp = selectedTiles[0].transform.position;

                        selectedTiles[0].transform.position = selectedTiles[1].transform.position;
                        selectedTiles[1].transform.position = temp;
                        selectedTiles[1].GetComponent<SpriteRenderer>().color =
                            selectedTiles[1].GetComponent<ConveyorSelector>().startColor;
                        ++swapsDone;
                    }
                }
            }

            selectedTiles[0].GetComponent<SpriteRenderer>().color = selectedTiles[0].GetComponent<ConveyorSelector>().startColor;

            selectedTiles.Clear();
        }
    }
    
    uint RotateLeft(uint x, int y)
    {
        uint k = (x << y);
        if(k > 8)
        {
            k = 1;
        }
        return k;
    }
    uint RotateRight(uint x, int y)
    {
        uint k = (x >> y);
        if(k < 1)
        {
            k = 8;
        }
        return k;
    }
    public void RotateTile(float degrees)
    {
       
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
        //Debug.Log("Hopefully shifting");
        if(degrees > 0)
        {
            //Debug.Log("shifting right");
            uint right = RotateRight((uint)shorten.direction, 1);
            Debug.Log("Right value is" + right);
            shorten.direction = (ConveyorDirection.Direction)right;
        }
        else
        {
            Debug.Log("shifting left");
            uint left =  RotateLeft((uint)shorten.direction, 1);
            shorten.direction = (ConveyorDirection.Direction)left;
        }

    }

    public void ToggleSpawning()
    {
        isRunning = !isRunning;
    }
    
}
