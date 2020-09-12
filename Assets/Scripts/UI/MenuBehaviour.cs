using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehaviour : MonoBehaviour
{
    int level = 0;
    public float waitTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// This function loads a scene.
    /// </summary>
    /// <param name="levelName">The int that is entered is
    /// the scene that is loaded.</param>
    public void LoadLevel(int levelName)
    {
        level = levelName;
        Invoke("LoadNext", waitTime);
    }

    public void LoadNext()
    {
        // Loads the next scene.
        UnityEngine.SceneManagement.SceneManager.LoadScene(level);
    }
}
