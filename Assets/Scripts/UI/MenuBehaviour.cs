using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehaviour : MonoBehaviour
{
    int level = 0;
    public float waitTime;
    public bool canHighLight = true;
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
        Time.timeScale = 1;
        // Loads the next scene.
        UnityEngine.SceneManagement.SceneManager.LoadScene(level);
    }

    public void PauseTime(int currentTime)
    {
        Time.timeScale = currentTime;
    }

    public void HighLightController(bool highLight)
    {
        canHighLight = highLight;
    }

    public void QuitGame()
    {
        // This if statement will quit the game after being
        // built and in the game panel in Unity.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
