using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script controlls the horizontal and vertical movement of the player
public class PlayerMovement : MonoBehaviour {

    private CharacterController character_Controller;

    private Vector3 move_Direction;

    public float speed = 5f;
    private float gravity = 20f;

    public float jump_Force = 10f;
    private float vertical_Velocity;

    void Awake()
    {
        character_Controller = GetComponent<CharacterController>();
    }
	
	void Update ()
    {
        MoveThePlayer();
	}

    void MoveThePlayer()
    {
        //enabling horizontal and vertical (jumping) movement
        move_Direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f,
                                     Input.GetAxis(Axis.VERTICAL));

        move_Direction = transform.TransformDirection(move_Direction);
        move_Direction *= speed * Time.deltaTime *Time.timeScale;

        ApplyGravity();

        character_Controller.Move(move_Direction);
    }

    void ApplyGravity()
    {
        //jumping speed
        vertical_Velocity -= gravity * Time.deltaTime;
        // jump
        PlayerJump();
        move_Direction.y = vertical_Velocity * Time.deltaTime * Time.timeScale;

    }
    //jump when we press space
    void PlayerJump()
    {
        if(character_Controller.isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            vertical_Velocity = jump_Force;
        }
    }
}