using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PowerTransmitter))]
public class LineVisualizer : MonoBehaviour
{
    [Tooltip("The object to spawn that will hold the LineRenderer")]
    public GameObject lineHolder;
    
    /// <summary>
    /// The transmitter to draw connection lines based on
    /// </summary>
    private PowerTransmitter transmitter;

    /// <summary>
    /// Keeps track of what wires have been drawn or not
    /// </summary>
    private List<GameObject> drawnWiresDestinations = new List<GameObject>();
    
    private List<LineRenderer> linesDrawn = new List<LineRenderer>();
    private IEnumerator DrawWires()
    {
        while (true)
        {

            // Loop through all connected machines
            foreach (var machine in transmitter.connected)
            {
                // Makes sure the wire does not already exist
                bool exists = false;
                
                foreach (var wire in drawnWiresDestinations)
                {
                    // If the destination is in the list, the wire already exists. Do not draw it again
                    if (wire.transform.Equals(machine.transform))
                    {
                        exists = true;
                        break;
                    }
                }
                
                
                // If the wire does not exist
                if (!exists)
                {
                    // Get the first child, which will always be the wire target
                    LineRenderer line = Instantiate(lineHolder, gameObject.transform).GetComponent<LineRenderer>();
                    // Draw the wire
                    line.SetPosition(0, gameObject.transform.GetChild(0).position);
                    line.SetPosition(1, machine.transform.GetChild(0).position);
                    // Add the wire destination to the list of spawned wires
                    drawnWiresDestinations.Add(machine.gameObject);
                    linesDrawn.Add(line);
                }
            }
            
            // The number of connected machines and number of wires drawn should be equal
            // If they are not, there are extra lines
            if (transmitter.connected.Count != drawnWiresDestinations.Count)
            {
                for (int i = 0; i < drawnWiresDestinations.Count; ++i)
                {
                    GameObject wire = drawnWiresDestinations[i];
                    bool hasPair = false;
                    foreach (var machine in transmitter.connected)
                    {
                        if (wire.Equals(machine.gameObject))
                        {
                            hasPair = true;
                            break;
                        }
                    }

                    if (!hasPair)
                    {
                        drawnWiresDestinations.Remove(wire);
                        Destroy(linesDrawn[i].gameObject);
                        linesDrawn.RemoveAt(i);
                    }
                }
            }
            
            yield return new WaitForSeconds(transmitter.tickRate);
        }
    }

    public void Start()
    {
        transmitter = gameObject.GetComponent<PowerTransmitter>();
        StartCoroutine(DrawWires());
    }
}
