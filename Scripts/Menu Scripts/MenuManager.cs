using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// the script responsible for changing scenes and loading the correct levels from the main menu
public class MenuManager : MonoBehaviour
{
    private void Awake()
    {
        //set timescale at 1. if this scene was loaded from a game level it would be 0.
        //now it always start at 1
        Time.timeScale = 1;
    }
    //the function used to load version1 of the map.we set the objectives collected at 0 here (its a static var)
    public void LoadSceneVersion1()
    {
        PlayerStats.numberOfCollectibles = 0;
        Cursor.visible = false;
        SceneManager.LoadScene("Map_v1");
    }
    public void LoadSceneVersion2()
    {
        PlayerStats.numberOfCollectibles = 0;
        Cursor.visible = false;
        SceneManager.LoadScene("Map_v2");
    }
    //the function used to load the main menu
    public void LoadMainMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
