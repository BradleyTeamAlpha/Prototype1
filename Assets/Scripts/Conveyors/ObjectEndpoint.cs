using UnityEngine;

public class ObjectEndpoint : MonoBehaviour
{
    [Tooltip("How many objects this endpoint needs to be full")]
    public int objectNeeded = 10;

    [Tooltip("How many objects this furnaces has obtained so far")]
    private int objectAquired = 0;

    /// <summary>
    /// Has this furnace updated the GameManager yet?
    /// </summary>
    private bool updatedGM = false;

    private GameManager gm;

    private void Start()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TargetObject"))
        {
            Destroy(other.gameObject);
            ++objectAquired;
        }
    }

    private void Update()
    {
        if (!updatedGM && objectAquired == objectNeeded)
        {
            updatedGM = true;
            gm.AddCompleteFurnace();
        }
    }
}
