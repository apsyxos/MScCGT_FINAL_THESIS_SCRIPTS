using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script controls the rate the footsteps are heard when we move on the map
public class PlayerFootsteps : MonoBehaviour {

    private AudioSource footstep_Sound;

    [SerializeField]
    private AudioClip[] footstep_Clip;

    private CharacterController character_Controller;

    [HideInInspector]
    public float volume_Min, volume_Max;

    private float accumulated_Distance;

    [HideInInspector]
    public float step_Distance;

	void Awake () {
        footstep_Sound = GetComponent<AudioSource>();

        character_Controller = GetComponentInParent<CharacterController>();
	}
	
	void Update () {
        CheckToPlayFootstepSound();	
	}

    void CheckToPlayFootstepSound() {

        // if we are NOT on the ground
        if (!character_Controller.isGrounded) 
            return;
            

        if(character_Controller.velocity.sqrMagnitude > 0)
        {
            // accumulated distance is the value how far can we go 
            // e.g. make a step or sprint
            // until we play the footstep sound
            accumulated_Distance += Time.deltaTime;
            //if the accumulated distance is more than the stepdistance, then we play a footstep clip from the list
            if(accumulated_Distance > step_Distance) {

                footstep_Sound.volume = Random.Range(volume_Min, volume_Max);
                footstep_Sound.clip = footstep_Clip[Random.Range(0, footstep_Clip.Length)];
                footstep_Sound.Play();
                //reset the accumulated distance to 0
                accumulated_Distance = 0f;
            }

        } else
        {
            accumulated_Distance = 0f;
        }
    }
}