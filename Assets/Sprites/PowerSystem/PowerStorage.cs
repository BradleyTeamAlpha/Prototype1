using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerStorage : PowerComponent
{
    [Header("Script Variables")]
    [Tooltip("How much power is currently in the object")]
    public float currentAmount = 0;
    
    [Tooltip("The max the object can hold. Excess power is lost")]
    public float maxPower = 100;
    
    
    /// <summary>
    /// Adds power to the object. Excess power is lost
    /// </summary>
    /// <param name="amount">How much power to add</param>
    /// <returns>True if the value was ceiled, false otherwise</returns>>
    private bool AddPower(float amount)
    {
        currentAmount += amount;

        if (currentAmount > maxPower)
        {
            currentAmount = maxPower;
            return true;
        }

        return false;

    }

    /// <summary>
    /// Remove power from the object, flooring at 0
    /// </summary>
    /// <param name="amount">How much power to remove</param>
    /// <returns>True if the value was floored, false otherwise</returns>
    private bool RemovePower(float amount)
    {
        currentAmount -= amount;

        if (currentAmount < 0)
        {
            currentAmount = 0;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Pull power from an object. If the amount requested is not available, empty the object
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public float PullPower(float amount)
    {
        // Cache the current power before removing from it
        float currentCache = currentAmount;
        bool hasFloored = RemovePower(amount);
        
        // If the value was floored, return the amount that was in before. Otherwise, return the requested amount
        return hasFloored ? currentCache : amount;
    }

    /// <summary>
    /// Push power to an object. Excess power is lost
    /// </summary>
    /// <param name="amount">How much power to push to the object</param>
    /// <returns>If the power added was ceilinged</returns>>
    public bool PushPower(float amount)
    {
        // TODO: Add more logic to pushing (and find what that logic should be)
        //float currentCache = currentAmount;

        bool hasCeil = AddPower(amount);
        return hasCeil;
    }
}
