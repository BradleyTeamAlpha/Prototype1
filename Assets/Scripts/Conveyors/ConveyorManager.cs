using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorManager : MonoBehaviour
{
    
    [Tooltip("Tiles the player has currently selected")]
    public List<GameObject> selectedTiles;
    
    
    /*
     * if list is length 2
     *     swap tiles
     *
     * if list is length 1 and button is hit
     *     do thing (rotate, undo etc...)
     */

    private void Update()
    {
        if (selectedTiles.Count == 2)
        {
            Vector3 temp = selectedTiles[0].transform.position;

            selectedTiles[0].transform.position = selectedTiles[1].transform.position;
            selectedTiles[1].transform.position = temp;
            
            selectedTiles.Clear();
        }
    }
}
