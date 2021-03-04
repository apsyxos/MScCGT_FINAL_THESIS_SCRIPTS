using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//script responsible for the game behaviour when we complete the game
public class LevelCompletedScript : MonoBehaviour
{
    //number of gameobjects that need to be turned off when we complete the game, in order to avoid bugs
    public GameObject HealthUI;
    public GameObject weapons;
    public GameObject crosshair;
    public GameObject gameCompletedMenu;
    public GameObject PauseMenu;

    void OnTriggerEnter(Collider other)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (other.gameObject.tag == "Player")
        {
            if(PlayerStats.numberOfCollectibles == 3)
            {
                //stop the time, freeze the game
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                crosshair.SetActive(false);
                weapons.SetActive(false);
                HealthUI.SetActive(false);
                gameCompletedMenu.SetActive(true);
                //deactivate the pausegame script
                PauseMenu.GetComponent<PauseGame>().enabled = false;
            }
        }
    }
    //we could use the MenuManager script to load the main menu scene. i prefer to have the option here though
    public void LevelCompleted()
    {
        SceneManager.LoadScene("Menu");
    }
}
