using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//enum with the variables for the enemy movement state
public enum EnemyState {
    PATROL,
    CHASE,
    ATTACK
}
//this is the main script that controls the enemy's movement and attack in the game
public class EnemyController : MonoBehaviour {

    private EnemyAnimator enemy_Anim;
    private NavMeshAgent navAgent;

    //the current state of the enemy
    private EnemyState enemy_State;

    //walking and running speed for the enemy
    public float walk_Speed = 0.5f;
    public float run_Speed = 4f;

    //if distance is less than that value, the zombie detects the player and starts chasing them
    public float chase_Distance = 7f;
    private float current_Chase_Distance;
    //the distance from which the enemy can attack
    public float attack_Distance = 1.8f;
    //if the player flees, it gives him the opportunity for them to put some distance
    //before the zombie starts chasing them
    public float chase_After_Attack_Distance = 2f;

    //the distance a zombie can move when randomly moving on the map
    public float patrol_Radius_Min = 20f, patrol_Radius_Max = 60f;
    //for how long the patroling in that particular direction will last
    public float patrol_For_This_Time = 15f;
    private float patrol_Timer;

    //attack delay for the zombie before it attacks the player
    public float wait_Before_Attack = 2f;
    private float attack_Timer;

    private Transform target;
    public GameObject attack_Point;
    private EnemyAudio enemyAudio;


    void Awake() {
        enemy_Anim = GetComponent<EnemyAnimator>();
        navAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
        enemyAudio = GetComponentInChildren<EnemyAudio>();
    }

    // Use this for initialization
    void Start () {
        //start patroling
        enemy_State = EnemyState.PATROL;
        //initialize the patrol_Timer with the patrol time
        patrol_Timer = patrol_For_This_Time;
        // when the enemy first gets to the player
        // attack right away
        attack_Timer = wait_Before_Attack;
        // memorize the value of chase distance
        // so that we can put it back
        current_Chase_Distance = chase_Distance;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(enemy_State == EnemyState.PATROL) {
            Patrol();
        }
        if(enemy_State == EnemyState.CHASE) {
            Chase();
        }
        if (enemy_State == EnemyState.ATTACK) {
            Attack();
        }
    }

    //the functio responsible for the random patrolling of the zombie
    void Patrol()
    {
        // tell nav agent that he can move
        navAgent.isStopped = false;
        navAgent.speed = walk_Speed;

        // add to the patrol timer
        patrol_Timer += Time.deltaTime;
        //if the timer for the patrol has reached its limit, start a new direction to patrol
        // and reset the timer
        if(patrol_Timer > patrol_For_This_Time)
        {
            SetNewRandomDestination();
            patrol_Timer = 0f;
        }
        //if the velocity of the enemy is not 0, then play the walking animation
        if(navAgent.velocity.sqrMagnitude > 0)
        {
            enemy_Anim.Walk(true);        
        } else
        {
            enemy_Anim.Walk(false);
        }

        // test the distance between the player and the enemy
        if(Vector3.Distance(transform.position, target.position) <= chase_Distance)
        {
            //not walking anymore
            enemy_Anim.Walk(false);
            //going to chase state to start chasing the player
            enemy_State = EnemyState.CHASE;
            // play spotted audio
            enemyAudio.PlayScreamSound();
        }
    }

    //the function responsible for chasing the player
    void Chase()
    {
        // enable the agent to move again
        navAgent.isStopped = false;
        navAgent.speed = run_Speed;
        // set the player's position as the destination
        // because we are chasing(running towards) the player
        navAgent.SetDestination(target.position);
        //if the velocity of the zombie is not 0
        if (navAgent.velocity.sqrMagnitude > 0)
        {
            enemy_Anim.Run(true);
        } else
        {
            enemy_Anim.Run(false);
        }
        // if the distance between enemy and player is less than attack distance
        if(Vector3.Distance(transform.position, target.position) <= attack_Distance) {
            // stop the animations
            enemy_Anim.Run(false);
            enemy_Anim.Walk(false);
            enemy_State = EnemyState.ATTACK;

            // reset the chase distance to previous
            if(chase_Distance != current_Chase_Distance)
            {
                chase_Distance = current_Chase_Distance;
            }
        }
        //if the player flees and distance between zombie-player is larger than chase_Distance, get back to patrolling
        else if(Vector3.Distance(transform.position, target.position) > chase_Distance)
        {
            // player run away from enemy
            // stop running
            enemy_Anim.Run(false);
            //get back to patrolling
            enemy_State = EnemyState.PATROL;
            // reset the patrol timer so that the function
            // can calculate the new patrol destination right away
            patrol_Timer = patrol_For_This_Time;
            // reset the chase distance to previous
            if (chase_Distance != current_Chase_Distance)
            {
                chase_Distance = current_Chase_Distance;
            }
        }
    }

    //function responsible for attacking the player
    void Attack()
    {
        //zombie has stopped moving
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;
        //the timer for the zombie attacks
        attack_Timer += Time.deltaTime;
        //enough time has passed to attack the player
        if(attack_Timer > wait_Before_Attack)
        {
            // play attack sound
            enemyAudio.PlayAttackSound();
            //play the attack animation
            enemy_Anim.Attack();
            //reset the timer for the next attack
            attack_Timer = 0f;
        }
        //if the player flees, chase them
        if(Vector3.Distance(transform.position, target.position) >
           attack_Distance + chase_After_Attack_Distance)
        {
            enemy_State = EnemyState.CHASE;
        }
    }
    //function that sets the destination in the patrol state
    void SetNewRandomDestination()
    {
        //the radius in which the zombie will move
        float rand_Radius = Random.Range(patrol_Radius_Min, patrol_Radius_Max);
        //random direction of the move
        Vector3 randDir = Random.insideUnitSphere * rand_Radius;
        randDir += transform.position;

        NavMeshHit navHit;
        //set the position of the new destination
        NavMesh.SamplePosition(randDir, out navHit, rand_Radius, -1);
        // follow the new destination
        navAgent.SetDestination(navHit.position);
    }
    //turn on the attack point on the zombie in the animation. it is responsible for damaging the player. it is an event in the attack animation of the zombie
    void Turn_On_AttackPoint()
    {
        attack_Point.SetActive(true);
    }
    void Turn_Off_AttackPoint()
    {
        if (attack_Point.activeInHierarchy)
        {
            attack_Point.SetActive(false);
        }
    }
    public EnemyState Enemy_State
    {
        get; set;
    }
} 