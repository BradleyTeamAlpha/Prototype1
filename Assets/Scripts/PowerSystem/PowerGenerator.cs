using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGenerator : PowerComponent
{
    
    [Header("Script Variables")]
    [Tooltip("How much power per tick the generator produces")]
    public float amountGenerated = 10;
    
    [Tooltip("How long in ticks each fuel lasts")]
    public int timeToGenerate = 10;
    
    [Tooltip("Is this generator creating power")]
    public bool isGenerating = false;
    
    [Tooltip("How much fuel is left in the generator")]
    public int fuelCount = 0;

    /// <summary>
    /// Creates power, and stores it in the generators internal buffer
    /// </summary>
    /// <param name="amount">The amount of power to add per tick</param>
    /// <param name="time">The time in ticks each fuel lasts. Power is generated evenly throughout the time</param>
    private IEnumerator GeneratePower(float amount, int time)
    {
        while (true)
        {
            // Checking for isGenerating at each level because it can be toggled at any time. There has to be a better way to do this
            if (isGenerating)
            {
                while (fuelCount > 0 && isGenerating)
                {
                    // Keep track of how many ticks have passed for burning this fuel
                    int ticksElapsed = 0;
                    --fuelCount;
                    
                    // Generate power until the correct amount has been generated
                    while (ticksElapsed <= time)
                    {
                        powerStorage.PushPower(amount);
                        ++ticksElapsed;
                        yield return new WaitForSeconds(tickRate);
                    }
                }
            }

            yield return new WaitForSeconds(tickRate);
        }
    }

    public override void Awake()
    {
        base.Awake();
        StartCoroutine(GeneratePower(amountGenerated, timeToGenerate));
    }
}
