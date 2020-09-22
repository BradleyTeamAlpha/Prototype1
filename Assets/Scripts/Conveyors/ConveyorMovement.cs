using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorMovement : MonoBehaviour
{
    [Tooltip("How long the object lives")]
    public float timeToLive = 400f;
    
    private ConveyorDirection.Direction direction;


    private int converyorsTouching = 1;
    private void Start()
    {
        Destroy(gameObject, timeToLive);
    }
    
    void OnTriggerStay2D(Collider2D movement)
    {
        //transform.Translate(Vector2.up * Time.deltaTime);
        // Coal should only move on conveyors, not all colliders
        if (!movement.gameObject.CompareTag("Conveyor"))
        {
            return;
        }
        switch (direction)
        {
            case ConveyorDirection.Direction.Left:
                transform.Translate((Vector2.left / converyorsTouching) * Time.deltaTime);
                break;
            case ConveyorDirection.Direction.Right:
                transform.Translate((Vector2.right / converyorsTouching) * Time.deltaTime);
                break;
            case ConveyorDirection.Direction.Up:
                transform.Translate((Vector2.up / converyorsTouching) * Time.deltaTime);
                break;
            case ConveyorDirection.Direction.Down:
                transform.Translate((Vector2.down / converyorsTouching) * Time.deltaTime);
                break;
        } 
        //Debug.Log(gameObject.name + " Touching " + converyorsTouching);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("XTile"))
        {
            Destroy(gameObject);
        }
        if (!other.gameObject.CompareTag("Conveyor"))
        {
            return;
        }

        ++converyorsTouching;
        //Debug.Log(gameObject.name + " Touching " + converyorsTouching);
        StartCoroutine(SetDirection(other.gameObject));
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Conveyor"))
        {
            --converyorsTouching;
            //Debug.Log(gameObject.name + " Touching " + converyorsTouching);
        }
        
    }

    private IEnumerator SetDirection(GameObject target)
    {
        Debug.Log("Starting direction set");
        yield return new WaitForSeconds(1.15f);
        direction = target.GetComponent<ConveyorDirection>().direction;
        transform.position *= 1.001f;
        Debug.Log("Direction set");
    }
}
