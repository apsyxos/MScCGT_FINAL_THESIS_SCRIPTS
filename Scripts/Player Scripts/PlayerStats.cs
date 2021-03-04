using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script includes the functions used to display the health and stamina bars
//and called in the HealthScript and PlayerSprint
//also define the number of collectibles the player has found as a static to use in the collectibles scripts
//it is an appropriate script to use in, since PlayerStats is in the GameScripts game object that is active throughout the mission
public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private Image healthStats, staminaStats;

    [HideInInspector]
    public static int numberOfCollectibles = 0;

    public void DisplayHealthStats(float healthValue)
    {
        //divided by 100 to make it a % 
        healthValue /= 100f;
        healthStats.fillAmount = healthValue;
    }

    public void DisplayStaminaStats(float staminaValue)
    {
        staminaValue /= 100f;
        staminaStats.fillAmount = staminaValue;
    }

}
