using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script controls the hearbeat sound depending the player's health
public class HeartBeatScript : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] heartBeat;

    //controls when the heartbeat sound plays. without it, the sound will play constantly and overlap since it is called on the update
    //with this bool it gets called only on the rate we defined in the coroutines
    private bool clipCanPlay;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        //we will start with the first clip on the list
        audioSource.clip = heartBeat[0];
        clipCanPlay = true;
    }


    void Update()
    {
        if(clipCanPlay == true)
            HeartBeat();
    }


    void HeartBeat()
    {
        //depending the health level the player has, it calls the appropriate heartbeat
        //instead of a nested if (like in the RealisticDamageIndicator script) we do three separate and we use the appropriate each time
        if(GetComponentInParent<HealthScript>().health <=61 && GetComponentInParent<HealthScript>().health>41)
        {
            //select the appropriate sound and play it
            audioSource.clip = heartBeat[0];
            audioSource.Play();
            //immediately disable the ability to play
            clipCanPlay = false;
            //coroutine controls the playing rate of the clip
            StartCoroutine(SlowHeartBeatDelay());

        }
        if (GetComponentInParent<HealthScript>().health <= 41 && GetComponentInParent<HealthScript>().health > 21)
        {
            audioSource.clip = heartBeat[1];
            audioSource.Play();

            clipCanPlay = false;

            StartCoroutine(FastHeartBeatDelay());

        }
        if (GetComponentInParent<HealthScript>().health <= 21)
        {
            audioSource.clip = heartBeat[2];
            audioSource.Play();

            clipCanPlay = false;

            StartCoroutine(FastHeartBeatDelay());

        }
    }

    //these are the coroutines that control the rate of the heartbeat
    //the time equals to the time length of each clip to avoid sound overlapping between beats
    //at the end we make clipCanPlay = true to allow for the next beat to play
    IEnumerator FastHeartBeatDelay()
    {
        yield return new WaitForSeconds(1.2f);
        clipCanPlay = true;
    }
    IEnumerator SlowHeartBeatDelay()
    {
        yield return new WaitForSeconds(1.8f);
        clipCanPlay = true;
    }
}
