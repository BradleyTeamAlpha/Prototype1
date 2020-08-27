using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PowerStorage))]
public class PowerComponent : MonoBehaviour
{
    /// <summary>
    /// How often, in seconds, the machine does its behaviour
    /// </summary>
    [Header("Base Variables")]
    public float tickRate = 0.5f;
    
    protected PowerStorage powerStorage;
    
    public virtual void Awake()
    {
        powerStorage = gameObject.GetComponent<PowerStorage>();
    }

}
