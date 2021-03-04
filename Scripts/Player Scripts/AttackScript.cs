using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the attack script for the AXE and the zombie.
//they work in a similar way, since they both have an attack point attached to them 
//that come into contact and deal damage
public class AttackScript : MonoBehaviour {

    public float damage = 150f;
    public float radius = 1f;
    public LayerMask layerMask;
	
	void Update ()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask);
        if(hits.Length > 0)
        {
            hits[0].gameObject.GetComponent<HealthScript>().ApplyDamage(damage);            
            gameObject.SetActive(false);
        }
	}
} 