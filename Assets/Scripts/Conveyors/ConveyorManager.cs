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
        
        if (selectedTiles.Count == 2)
        {
            if (!selectedTiles[0].Equals(selectedTiles[1]))
            {
                Vector3 temp = selectedTiles[0].transform.position;

                selectedTiles[0].transform.position = selectedTiles[1].transform.position;
                selectedTiles[1].transform.position = temp;
                selectedTiles[1].GetComponent<SpriteRenderer>().color = selectedTiles[1].GetComponent<ConveyorSelector>().startColor;
                ++swapsDone;
            }

            selectedTiles[0].GetComponent<SpriteRenderer>().color = selectedTiles[0].GetComponent<ConveyorSelector>().startColor;

            selectedTiles.Clear();
        }
    }
}
