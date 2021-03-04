using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this script is responsible for switching between the axe and the pistol
public class WeaponManager : MonoBehaviour {

    //the weapon objects that will be used
    [SerializeField]
    private WeaponHandler[] weapons;

    private int current_Weapon_Index;

    void Start ()
    {
        current_Weapon_Index = 0;
        //equiping the first weapon in the list
        weapons[current_Weapon_Index].gameObject.SetActive(true);
	}
	
	void Update ()
    {
        WeaponChoice();
    }

    //this function inputs the buttons used to change weapons
    void WeaponChoice()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TurnOnSelectedWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TurnOnSelectedWeapon(1);
        }
    }

    //this script is responsible for switching weapons
    void TurnOnSelectedWeapon(int weaponIndex) {
        //if the weapon equipped is already the one we are trying to equip, then return
        if (current_Weapon_Index == weaponIndex)
            return;

        // turn off the current weapon
        weapons[current_Weapon_Index].gameObject.SetActive(false);
        // turn on the selected weapon
        weapons[weaponIndex].gameObject.SetActive(true);
        // store the current selected weapon index
        current_Weapon_Index = weaponIndex;
    }

    //used in the PlayerAttack script to return the current weapon when we need to use it
    public WeaponHandler GetCurrentSelectedWeapon() {
        return weapons[current_Weapon_Index];
    }
}