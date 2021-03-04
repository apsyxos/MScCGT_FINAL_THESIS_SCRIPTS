using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCollision : MonoBehaviour
{
    //check if the collectible collides with the player, to add to the collected objectives
    void OnTriggerEnter(Collider other)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (other.gameObject.tag == "Player")
        {
            ++PlayerStats.numberOfCollectibles;
            this.gameObject.SetActive(false);
        }
        
    }
}
