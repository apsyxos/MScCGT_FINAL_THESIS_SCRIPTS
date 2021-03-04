using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script handles the RPDS described. It is one of the most important scripts
//nevertheless, the idea behind it is simple. While the player goes below a certain health level, 
//a new effect gets activated


public class RealisticDamageIndicator : MonoBehaviour
{
    //camera gameobject used in the "limping" effect
    [SerializeField]
    private GameObject mainCamera;

    //the blood effect with the alpha that gets stronger the more the player gets hurt
    [SerializeField]
    private Image bloodUI;
    float alpha = 0.0f;

    private Animator zoomCameraAnim;
    private GameObject crosshair;
    private PlayerMovement playerMovement;

    void Awake()
    {
        //at the start of the game, we initialise the color with alpha 0
        bloodUI.GetComponent<Image>().color = new Color(1,1,1,alpha);
        //make sure the animator for the flashing effect of the blood is disabled
        bloodUI.GetComponent<Image>().GetComponent<Animator>().enabled = false;

        zoomCameraAnim = transform.Find(Tags.LOOK_ROOT)
                                  .transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();
        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);
        playerMovement = GetComponent<PlayerMovement>();

    }


    void Update()
    {
        DamageReaction();
        //refreshes the blood effect color with the updated alpha (could also be put at the end of DamageReaction function)
        bloodUI.GetComponent<Image>().color = new Color(1, 1, 1, alpha);
    }

    void DamageReaction()
    {
        //if the player's health is less than 80% increase the alpha of the blood hue
        if (this.GetComponent<HealthScript>().health <= 81)
        {
            alpha = 0.2f;

            //if the player's health is less than 60% increase the alpha and remove the ability to sprint
            if (this.GetComponent<HealthScript>().health <= 61)
            {
                alpha = 0.4f;
                this.GetComponent<PlayerSprint>().canSprint = false;

                //if health is less than 40%, increase the blood hue, activate the animation for the blood hue and use the limping mechanics and animation
                if (this.GetComponent<HealthScript>().health <= 41)
                {
                    bloodUI.GetComponent<Image>().GetComponent<Animator>().enabled = true;
                    alpha = 0.6f;

                    //while the player is walking in any direction, using WASD, the animation is active
                    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                    {
                        mainCamera.GetComponent<Animator>().enabled = true;
                    }
                    else
                    {
                        //if the player is not walking, firstly reset the rotation to 0 and then disable the limping animation
                        //if the rotation is not disabled, the player camera will look tilted
                        mainCamera.transform.localRotation = Quaternion.Euler(0, 0, 0);
                        mainCamera.GetComponent<Animator>().enabled = false;
                    }

                    //if health is 20% or less, increase alpha and remove the ability to aim down the sights
                    if (this.GetComponent<HealthScript>().health <= 21)
                    {
                        alpha = 0.8f;

                        //if the player is aiming the weapon while they drop to that health level
                        //the weapon will get stuck in the aiming animation
                        //the next three lines fix that bug by reverting the animation and restoring the crosshair to use
                        zoomCameraAnim.Play(AnimationTags.ZOOM_OUT_ANIM);
                        crosshair.SetActive(true);
                        playerMovement.speed = this.GetComponent<PlayerSprint>().move_Speed;

                        this.GetComponent<PlayerAttack>().canAim = false;
                    }
                }
            }
        }
    }
}