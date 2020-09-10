using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorMovement : MonoBehaviour
{
    private ConveyorDirection cd;
    void OnTriggerStay2D(Collider2D movement)
    {
        //transform.Translate(Vector2.up * Time.deltaTime);
        
        if (cd != null)
        {
            switch (cd.shifter)
            {

                case ConveyorDirection.bitShiftMayhem.Left:
                    transform.Translate(Vector2.left * Time.deltaTime);
                    Debug.Log("going left");
                    break;
                case ConveyorDirection.bitShiftMayhem.Right:
                    transform.Translate(Vector2.right * Time.deltaTime);
                    Debug.Log("going right");
                    break;
                case ConveyorDirection.bitShiftMayhem.Up:
                    transform.Translate(Vector2.up * Time.deltaTime);
                    Debug.Log("going up");
                    break;
                case ConveyorDirection.bitShiftMayhem.Down:
                    transform.Translate(Vector2.down * Time.deltaTime);
                    Debug.Log("going down");
                    break;

                default:
                    break;


            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(SetDirection(other.gameObject));
    }

    private IEnumerator SetDirection(GameObject target)
    {
        yield return new WaitForSeconds(0.9f);
        cd = target.GetComponent<ConveyorDirection>();
        transform.position *= 1.001f;
    }
}
