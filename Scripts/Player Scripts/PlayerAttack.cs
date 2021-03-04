using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this script is responsible for the player's attack
// it handles the weapon firing and zooming
public class PlayerAttack : MonoBehaviour {

    private WeaponManager weapon_Manager;

    public float bulletDamage = 50f;

    private Animator zoomCameraAnim;
    private bool zoomed;
    private Camera mainCam;
    private GameObject crosshair;

    [SerializeField]
    private GameObject bloodSplatter;

    //if false, the player can not aim with the weapon
    //will be used when the player is damaged
    public bool canAim;
    private PlayerMovement playerMovement;

    //this is when we aim, we synchronize the walking speed with the walking sound
    public bool aiming;

    void Awake()
    {
        weapon_Manager = GetComponent<WeaponManager>();
        zoomCameraAnim = transform.Find(Tags.LOOK_ROOT)
                                  .transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();
        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);
        mainCam = Camera.main;
        canAim = true;
        playerMovement = GetComponent<PlayerMovement>();
        aiming = false;
    }

	void Update ()
    {
        WeaponShoot();
        ZoomInAndOut();
    }

    //this function controls the weapon's attack, whether it is the axe or the pistol
    void WeaponShoot()
    {  
        //if we press left mouse button
        if(Input.GetMouseButtonDown(0))
        {
           // handle axe
           //if the current weapon is tagged AXE then do the attack animation of that weapon
           if(weapon_Manager.GetCurrentSelectedWeapon().tag == Tags.AXE_TAG) {
               weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
           }
           // handle shoot
           //if the weapon is bullet type, do the appropriate attack of that weapon and call the 
           //BulletFired function that checks if we hit the enemy
           if(weapon_Manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.BULLET)
           {
               weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
               BulletFired();
           } 
        }    
    }
    //this function controls the weapon zooming
    public void ZoomInAndOut()
    {
        // we are going to aim with our camera on the weapon
        if(weapon_Manager.GetCurrentSelectedWeapon().weapon_Aim == WeaponAim.AIM && canAim == true)
        {
            // if we press and hold right mouse button
            //then we disable sprint and crosshair, do the zoom in animation and move at aim speed
            if(Input.GetMouseButtonDown(1))
            {
                this.GetComponent<PlayerSprint>().canSprint = false;
                aiming = true;
                zoomCameraAnim.Play(AnimationTags.ZOOM_IN_ANIM);
                crosshair.SetActive(false);
                playerMovement.speed = this.GetComponent<PlayerSprint>().aim_Speed;
            }
            // when we release the right mouse button click
            //revert to our previous state
            if (Input.GetMouseButtonUp(1))
            {
                this.GetComponent<PlayerSprint>().canSprint = true;
                aiming = false;
                zoomCameraAnim.Play(AnimationTags.ZOOM_OUT_ANIM);
                crosshair.SetActive(true);
                playerMovement.speed = this.GetComponent<PlayerSprint>().move_Speed;
            }
        }
    }

    //this function checks what is hit with when we fire our pistol
    void BulletFired()
    {
        //shoot a ray from our pistol. if the target is tagged as enemy, then we apply the bullet damage to their health script
        RaycastHit hit;
        if(Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit))
        {
            if(hit.transform.tag == Tags.ENEMY_TAG)
            {
                hit.transform.GetComponent<HealthScript>().ApplyDamage(bulletDamage);
                //instantiate a blood splatter on the point the enemy was hit. purely visual effect as an indication to the player that they hit the zombie
                var rot = Quaternion.FromToRotation(Vector3.forward, hit.normal);
                Instantiate(bloodSplatter, hit.point, rot);
            }
        }
    }
}