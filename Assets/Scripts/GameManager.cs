using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Tooltip("How many full furnaces are needed to win")]
    public int furnacesToWin = 1;

    /// <summary>
    /// How many furnaces are currently complete
    /// </summary>
    private int furnacesComplete = 0;

    [Tooltip("Object to show when the player wins the level")]
    public GameObject winObject;
    
    
    /// <summary>
    /// Increase the number of completed furnaces by 1
    /// </summary>
    public void AddCompleteFurnace()
    {
        ++furnacesComplete;
    }

    private void Update()
    {
        if (furnacesComplete >= furnacesToWin)
        {
            WinLevel();
        }
        
    }

    /// <summary>
    /// Win the level. Add more logic as needed
    /// </summary>
    private void WinLevel()
    {
        winObject.SetActive(true);
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Restart the current level. Add more logic as needed
    /// </summary>
    public void RestartLevel()
    {
        Time.timeScale = 1;
        LoadLevel(SceneManager.GetActiveScene().name);
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
