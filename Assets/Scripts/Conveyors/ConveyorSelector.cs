using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorSelector : MonoBehaviour
{

    public ConveyorManager manager;

    private void Start()
    {
        manager = GameObject.FindWithTag("TileManager").GetComponent<ConveyorManager>();
    }
    
    private void OnMouseOver()
    {
        Debug.Log($"Mousing over {gameObject}");
    }

    private void OnMouseDown()
    {
        Debug.Log($"Mouse down on {gameObject}");
        manager.selectedTiles.Add(gameObject);
    }

    private void OnMouseExit()
    {
        Debug.Log($"Mouse exit {gameObject}");
    }
}
