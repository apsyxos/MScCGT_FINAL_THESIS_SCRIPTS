using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//this script manages the health of the player and the zombies
public class HealthScript : MonoBehaviour
{

    private EnemyAnimator enemy_Anim;
    private NavMeshAgent navAgent;
    private EnemyController enemy_Controller;

    public float health = 100f;

    //important bools that distinguish the player and the zombie for the script behaviour
    public bool is_Player, is_Zombie;

    private bool is_Dead;
    private EnemyAudio enemyAudio;
    private PlayerStats playerStats;

    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private GameObject pauseMenuScript;

    [SerializeField]
    private AudioSource music;

	void Awake () {
	    
        if( is_Zombie) {
            enemy_Anim = GetComponent<EnemyAnimator>();
            enemy_Controller = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();
            enemyAudio = GetComponentInChildren<EnemyAudio>();
        }

        if(is_Player) {
            playerStats = GetComponent<PlayerStats>();
        }

	}
	
    //this function handles the damage dealt to either the player or the zombie
    //called in PlayerAttack and AttackScript scripts when the player uses the gun or the axe
    public void ApplyDamage(float damage) {

        // if the player (or zombie) died don't execute the rest of the code
        if (is_Dead)
            return;
        //apply damage to health
        health -= damage;

        if(is_Player) {
            // show the stats(display the health UI value)
            playerStats.DisplayHealthStats(health);
        }

        if(is_Zombie) {
            //if the zombie is damaged from a distance, and if he is patrolling
            //change the chase_Distance to 200, so that it will start chasing the player.
            //otherwise the player would kill the zombies from a distance and they wouldnt react
            if(enemy_Controller.Enemy_State == EnemyState.PATROL) {
                enemy_Controller.chase_Distance = 200f;
            }
        }

        //if health reaches 0, player (or enemy) has died
        if(health <= 0f) {

            PlayerDied();
            is_Dead = true;
        }

    }

    void PlayerDied() {

        //if it is the zombie
        if(is_Zombie) {
            //stop moving
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            enemy_Controller.enabled = false;
            //play the dead animation
            enemy_Anim.Dead();
            //play enemy death sound
            StartCoroutine(DeadSound());
            //deactivate the zombie after 10 seconds
            Invoke("TurnOffGameObject", 10f);

            //disable box collider. if we keep them,
            //we can keep shooting the area over the corpse and still have 
            //the blood coming out. its a bug
            this.GetComponent<BoxCollider>().enabled = false;
        }

        //if it is the player
        if (is_Player)
        {
            //put the camera lower to mimic the player falling down
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.9f, transform.position.z);

            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);
            //all the zombies stop moving            
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }

            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);
            //stop the timeScale
            Time.timeScale = 0f;
            //game music gets deactivated
            music.enabled = false;
            //game over ui
            gameOverScreen.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            //disable the pause game menu after the player died
            //otherwise we can press pause while the gameover ui is on 
            // and have overlaping uis
            pauseMenuScript.GetComponent<PauseGame>().enabled = false ;
        }
    }

    void TurnOffGameObject() {
        gameObject.SetActive(false);
    }

    //when the zombie dies play the appropriate sound
    //with a small delay to avoid sound clipping
    IEnumerator DeadSound()
    {
        yield return new WaitForSeconds(0.3f);
        enemyAudio.PlayDeathSound();
    }
}