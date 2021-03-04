using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script controls the behaviour of the player's movement when they sprint, walk and aim in the game

public class PlayerSprint : MonoBehaviour
{

    private PlayerMovement playerMovement;

    public float sprint_Speed = 10f;
    public float move_Speed = 5f;
    //its used in PlayerAttack. I could have defined it there, but i prefer it here with the rest of movement speeds
    public float aim_Speed = 2f;

    private PlayerFootsteps player_Footsteps;

    //footstep sounds when sprinting
    private float sprint_Volume = 1f;
    //footsteps sounds when walking
    private float walk_Volume_Min = 0.2f, walk_Volume_Max = 0.6f;

    //they are used to determine the rate of which we will hear the player's footsteps (see PlayerFootsteps script)
    //depending whether we are walking or running
    private float walk_Step_Distance = 0.4f;
    private float sprint_Step_Distance = 0.25f;

    private PlayerStats playerStats;
    //max value of sprint stamina
    private float sprint_Value = 100f;
    public float sprint_Threshold = 10f;

    //check if the player can sprint
    //will be used as false when the player gets damaged
    public bool canSprint;

	void Awake () {

        playerMovement = GetComponent<PlayerMovement>();

        player_Footsteps = GetComponentInChildren<PlayerFootsteps>();

        playerStats = GetComponent<PlayerStats>();

        canSprint = true;
	}
    void Start() {
        player_Footsteps.volume_Min = walk_Volume_Min;
        player_Footsteps.volume_Max = walk_Volume_Max;
        player_Footsteps.step_Distance = walk_Step_Distance;
    }
 
    void Update ()
    {
        Sprint();
	}

    //the function that determines when the player can sprint
    void Sprint() {

        //if the player is not aiming, then we can execute sprint when permitted
        if(this.GetComponent<PlayerAttack>().aiming == false)
        {
            // if we have stamina and we can sprint
            if (sprint_Value > 0f && canSprint == true )
            {
                //if we press Shift
                if (Input.GetKeyDown(KeyCode.LeftShift) )
                {
                    PlayerSprintSpeed();
                }
            }

            //if we release Shift we stop sprinting
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                PlayerMoveSpeed();
            }

            //while Shift+W and we can sprint, then we spend stamina
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && canSprint == true)
            {
                //stamina is getting spent. Every time we see timeScale is to pause when we pause the game
                sprint_Value -= sprint_Threshold * Time.deltaTime * Time.timeScale;

                //if stamina <=0 we stop sprinting and move with regular speed
                if (sprint_Value <= 0f)
                {
                    sprint_Value = 0f;
                    // reset the speed and sound
                    PlayerMoveSpeed();
                }
                //we still have stamina to spend, so we are sprinting
                else
                {
                    PlayerSprintSpeed();
                }

                //no matter what our state is, we display our current stamina level
                playerStats.DisplayStaminaStats(sprint_Value);
            }

            //if we press Shift+A/S/D we DO NOT SPRINT. otherwise the player would be sprinting sideways or backwards
            else if((Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))))
            {
                //this ensures that the stamina value and bar are replenished since we are not sprinting otherwise it the stamina would get spent since we press Shift
                ReplenishStamina();

                PlayerMoveSpeed();
            }

            //for any other button combination, we replenish our stamina
            else
            {
                if (sprint_Value != 100f)
                {
                    //the rate in which the stamina gets restored. we made it half of when spent
                    sprint_Value += (sprint_Threshold / 2f) * Time.deltaTime * Time.timeScale;
                    //display the stamina value
                    playerStats.DisplayStaminaStats(sprint_Value);
                    //lock the max stamina value to 100
                    if (sprint_Value > 100f)
                    {
                        sprint_Value = 100f;
                    }
                }
            }
        }

        //if a player can aim, all the buttons and commands get overriden, and the player moves at aiming speed (when aiming)
        else
        {
            //this if ensures that the stamina value and bar are replenished since we are not sprinting
            ReplenishStamina();

            playerMovement.speed = aim_Speed * Time.timeScale;
            player_Footsteps.step_Distance = walk_Step_Distance;
            player_Footsteps.volume_Min = walk_Volume_Min;
            player_Footsteps.volume_Max = walk_Volume_Max;
        }

    }

    //the walking speed of the player, along with the sound from the footsteps
    void PlayerMoveSpeed()
    {
        playerMovement.speed = move_Speed * Time.timeScale;
        player_Footsteps.step_Distance = walk_Step_Distance;
        player_Footsteps.volume_Min = walk_Volume_Min;
        player_Footsteps.volume_Max = walk_Volume_Max;
    }
    //the sprinting speed of the player, along with the sound from the footsteps
    void PlayerSprintSpeed()
    {
        playerMovement.speed = sprint_Speed * Time.timeScale;
        player_Footsteps.step_Distance = sprint_Step_Distance;
        player_Footsteps.volume_Min = sprint_Volume;
        player_Footsteps.volume_Max = sprint_Volume;
    }

    void ReplenishStamina()
    {
        if (sprint_Value < 100)
        {
            sprint_Value += (sprint_Threshold / 2f) * Time.deltaTime * Time.timeScale;
            playerStats.DisplayStaminaStats(sprint_Value);
        }
    }
}



























