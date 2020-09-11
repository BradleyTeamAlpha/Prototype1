using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorMovement : MonoBehaviour
{
    private ConveyorDirection.Direction direction;
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
                transform.Translate(Vector2.left * Time.deltaTime);
                break;
            case ConveyorDirection.Direction.Right:
                transform.Translate(Vector2.right * Time.deltaTime);
                break;
            case ConveyorDirection.Direction.Up:
                transform.Translate(Vector2.up * Time.deltaTime);
                break;
            case ConveyorDirection.Direction.Down:
                transform.Translate(Vector2.down * Time.deltaTime);
                break;
        } 
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
        StartCoroutine(SetDirection(other.gameObject));
    }

    private IEnumerator SetDirection(GameObject target)
    {
        yield return new WaitForSeconds(0.9f);
        direction = target.GetComponent<ConveyorDirection>().direction;
        transform.position *= 1.001f;
    }
}
