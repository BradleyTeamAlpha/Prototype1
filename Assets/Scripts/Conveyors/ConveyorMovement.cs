using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //objects will move themselves; on collider enter, move forward x long at x speed

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D movement)
    {
        //transform.Translate(Vector2.up * Time.deltaTime);
        ConveyorDirection cd = movement.gameObject.GetComponent<ConveyorDirection>();
        //Debug.Log(cd.shifter);
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
