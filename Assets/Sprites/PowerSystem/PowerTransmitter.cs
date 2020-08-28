using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTransmitter : PowerComponent
{
    [Header("Script Variables")]
    [Tooltip("Objects this one is pushing power to")]
    public List<PowerComponent> connected = new List<PowerComponent>();

    /// <summary>
    /// What voltage the transmitter is operating at. Voltages must be the same, or the reciever a transformer, for
    /// a transfer to occur
    /// </summary>
    public enum Voltages
    {
        VOLTAGES_NONE,
        VOLTAGES_LOW,
        VOLTAGES_HIGH
    }

    [Tooltip("What voltage the object is sending at")]
    public Voltages voltage = Voltages.VOLTAGES_HIGH;

    [Tooltip("Is the object able to transform between voltages")]
    public bool isTransformer = false;
    
    [Tooltip("Percentage of power that gets kept when pushing")]
    [Range(0, 1)]
    public float efficiency = 0.95f;

    [Tooltip("Can the object transmit power. True if yes, false if no")]
    public bool canTransmit = true;
    
    /// <summary>
    /// Pushes out a max of (maxPower / destinations) from each connected device
    /// </summary>
    /// <returns></returns>
    private IEnumerator DeliverPower()
    {
        // Continuously pull power from all connected objects
        while (true)
        {
            // The most the object can push is limited so each connected object gets the same total power.
            float maxPush = powerStorage.currentAmount / connected.Count;
            foreach (var destination in connected)
            {
                // This object asks the connected object to pull power
                destination.GetComponent<PowerTransmitter>().ReceivePower(this, maxPush);
            }

            yield return new WaitForSeconds(tickRate);
        }
    }

    /// <summary>
    /// Receive power pushed from another transmitter
    /// </summary>
    /// <param name="source">The transmitter that pushed the power</param>>
    /// <param name="amount">How much power was received</param>
    private void ReceivePower(PowerTransmitter source, float amount)
    {
        // Make sure the two objects are on the same voltage
        if (isTransformer || (source.voltage == voltage))
        {
            // Adjusts power based on sources efficiency
            float efficiencyAdjusted = amount * source.efficiency;
            // Pulling entire amount due to efficiency losses
            source.powerStorage.PullPower(amount);
            // Adding the adjusted amount due to efficiency losses
            powerStorage.PushPower(efficiencyAdjusted);
        }
        else
        {
            //Debug.Log("Voltages don't match, not sending power");
            // TODO: Make stuff go boom
        }
    }

    public void Start()
    {
        base.Awake();
        if (canTransmit)
            StartCoroutine(DeliverPower());
    }
}
