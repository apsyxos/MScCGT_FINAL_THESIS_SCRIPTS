using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script controls the behaviour when we pause the game while we play
public class PauseGame : MonoBehaviour
{
    public static bool gamePaused = false;
    //number of gameobjects that need to be activated/deactivated when the pause button is pressed
    public GameObject pauseMenu;
    public GameObject HealthUI;
    public GameObject weapons;
    public GameObject crosshair;

    void Update()
    {
        PauseOrUnpauseGame();
    }
    void PauseOrUnpauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused == false)
            {
                Time.timeScale = 0;
                gamePaused = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                crosshair.SetActive(false);
                weapons.SetActive(false);
                HealthUI.SetActive(false);
                pauseMenu.SetActive(true);
            }
            else
            {
                pauseMenu.SetActive(false);
                HealthUI.SetActive(true);
                weapons.SetActive(true);
                crosshair.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                gamePaused = false;
                Time.timeScale = 1;
            }
        }
    }
    //if we go to the main main menu through Main Menu button, it unfreezes the time.timescale so we can start again if we want
    public void UnFreeze()
    {
        gamePaused = false;
        Time.timeScale = 1;
    }
}
