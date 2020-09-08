﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorSelector : MonoBehaviour
{

    [Tooltip("Reference to the player manager")]
    public ConveyorManager manager;


    /// <summary>
    /// Reference to the object's sprite renderer
    /// </summary>
    private SpriteRenderer sr;
    
    [Tooltip("The starting color of the object")]
    public Color startColor;
    
    private void Start()
    {
        manager = GameObject.FindWithTag("TileManager").GetComponent<ConveyorManager>();
        sr = GetComponent<SpriteRenderer>();
        startColor = sr.color;
    }
    
    /// <summary>
    /// Darkens the tile on mouse over
    /// </summary>
    private void OnMouseEnter()
    {
        Color newColor = sr.color;
        newColor.r -= 0.5f;
        newColor.g -= 0.5f;
        newColor.b -= 0.5f;
        sr.color = newColor;
    }

    /// <summary>
    /// Adds the selected tile to the selected list
    /// </summary>
    private void OnMouseDown()
    {
        manager.selectedTiles.Add(gameObject);
    }

    /// <summary>
    /// Resets the color on mouse exit
    /// </summary>
    private void OnMouseExit()
    {
        sr.color = startColor;
    }
}
