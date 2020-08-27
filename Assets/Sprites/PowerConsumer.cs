using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerConsumer : PowerComponent
{

    
    /// <summary>
    /// Is the object consuming power, true is yes, false is no
    /// </summary>
    private bool isRunning = false;
    
    [Header("Script Variables")]
    [Tooltip("Should the object be consuming power, true is yes, false is no")]
    public bool isOn = false;
    
    [Tooltip("How much power the object consumes per tick")]
    public float consumeRate = 0f;
    
    /// <summary>
    /// Consumes power from the object. Consumes the given amount once per tick
    /// </summary>
    /// <param name="amount">How much power to consume per tick</param>
    private IEnumerator ConsumePower(float amount)
    {
        while (true)
        {
            if (powerStorage.currentAmount >= amount && isOn)
            {
                Debug.Log($"Removing {amount} power!");
                powerStorage.currentAmount -= amount;
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }
            
            yield return new WaitForSeconds(tickRate);
        }
    }

    public override void Awake()
    {
        base.Awake();
        StartCoroutine(ConsumePower(consumeRate));
    }
    
    /// <summary>
    /// Gets if the machine is running or not
    /// </summary>
    /// <returns>True if the machine is running, false otherwise</returns>
    public bool GetRunning()
    {
        return isRunning;
    }

    /// <summary>
    /// Gets if the machine is supposed to be running or not
    /// </summary>
    /// <returns>True if the object is supposed to be running, false otherwise</returns>
    public bool GetOn()
    {
        return isOn;
    }

    /// <summary>
    /// Get how much power per tick the object is consuming
    /// </summary>
    /// <returns>How much power/tick the object is consuming</returns>
    public float GetConsumeRate()
    {
        return consumeRate;
    }
}
