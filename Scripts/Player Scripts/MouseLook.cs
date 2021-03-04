using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

    [SerializeField]
    private Transform playerRoot, lookRoot;

    [SerializeField]
    private bool invert;

    [SerializeField]
    private bool can_Unlock = true;

    [SerializeField]
    private float sensivity = 1f;

    //the angles the mouse is locked into when looking up and down
    [SerializeField]
    private Vector2 default_Look_Limits = new Vector2(-70f, 80f);

    private Vector2 look_Angles;

    private Vector2 current_Mouse_Look;

    void Start ()
    {
        //at the beginning we start with locked mouse cursor and zeroed rotations
        Cursor.lockState = CursorLockMode.Locked;
        lookRoot.localRotation = Quaternion.Euler(0f, 0f, 0f);
        playerRoot.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }
	
	void Update ()
    {
        //while the cursor is locked, we can look around with the mouse
        //if not, we move the cursor. it happens on the menus
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            LookAround();
        }
	}

    void LookAround() {

        current_Mouse_Look = new Vector2(
            Input.GetAxis(MouseAxis.MOUSE_Y), Input.GetAxis(MouseAxis.MOUSE_X));

        //horizontal and vertical angles the mouse can look
        look_Angles.x += current_Mouse_Look.x * sensivity * (invert ? 1f : -1f) * Time.timeScale;
        look_Angles.y += current_Mouse_Look.y * sensivity * Time.timeScale;

        look_Angles.x = Mathf.Clamp(look_Angles.x, default_Look_Limits.x, default_Look_Limits.y);

        //look on the horizontal
        lookRoot.localRotation = Quaternion.Euler(look_Angles.x, 0f, 0f);
        //look on the vertical
        playerRoot.localRotation = Quaternion.Euler(0f, look_Angles.y, 0f);
    }
}