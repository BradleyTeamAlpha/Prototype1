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
        
        switch (direction)
        {
            case ConveyorDirection.Direction.Left:
                transform.Translate(Vector2.left * Time.deltaTime);
                Debug.Log("going left");
                break;
            case ConveyorDirection.Direction.Right:
                transform.Translate(Vector2.right * Time.deltaTime);
                Debug.Log("going right");
                break;
            case ConveyorDirection.Direction.Up:
                transform.Translate(Vector2.up * Time.deltaTime);
                Debug.Log("going up");
                break;
            case ConveyorDirection.Direction.Down:
                transform.Translate(Vector2.down * Time.deltaTime);
                Debug.Log("going down");
                break;
        } 
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(SetDirection(other.gameObject));
    }

    private IEnumerator SetDirection(GameObject target)
    {
        yield return new WaitForSeconds(0.9f);
        direction = target.GetComponent<ConveyorDirection>().direction;
        transform.position *= 1.001f;
    }
}
