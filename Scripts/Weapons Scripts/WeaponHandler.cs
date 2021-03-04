﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this script handles events that happen in the annimation of the weapons when they are used

public enum WeaponAim {
    NONE,
    AIM
}

public enum WeaponFireType {
    SINGLE,
    MULTIPLE
}

public enum WeaponBulletType {
    BULLET,
    NONE
}

public class WeaponHandler : MonoBehaviour {

    private Animator anim;

    public WeaponAim weapon_Aim;

    [SerializeField]
    private GameObject muzzleFlash;

    [SerializeField]
    private AudioSource shootSound, reload_Sound;

    public WeaponFireType fireType;

    public WeaponBulletType bulletType;

    public GameObject attack_Point;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    //these are functions that are used in the animations of the weapons. depending their
    // content, they either activate events in the animations, activate objects, or play the appropriate clip 
    public void ShootAnimation()
    {
        anim.SetTrigger(AnimationTags.SHOOT_TRIGGER);
    }

    public void Aim(bool canAim)
    {
        anim.SetBool(AnimationTags.AIM_PARAMETER, canAim);
    }

    void Turn_On_MuzzleFlash()
    {
        muzzleFlash.SetActive(true);
    }

    void Turn_Off_MuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }

    void Play_ShootSound()
    {
        shootSound.Play();
    }

    void Play_ReloadSound()
    {
        reload_Sound.Play();
    }

    void Turn_On_AttackPoint()
    {
        attack_Point.SetActive(true);
    }

    void Turn_Off_AttackPoint()
    {
        if(attack_Point.activeInHierarchy)
        {
            attack_Point.SetActive(false);
        }
    }
}