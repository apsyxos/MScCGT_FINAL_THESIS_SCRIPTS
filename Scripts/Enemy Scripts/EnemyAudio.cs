using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is responsible for managing the enemy sounds.
//the Play functions play the appropriate clips. 
// the variables are used to add the audio the enemy will use
public class EnemyAudio : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] screamClips;

    [SerializeField]
    private AudioClip dieClip;

    [SerializeField]
    private AudioClip[] attackClips;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayScreamSound()
    {
        audioSource.clip = screamClips[Random.Range(0, screamClips.Length)];
        audioSource.Play();
    }
    public void PlayAttackSound()
    {
        audioSource.clip = attackClips[Random.Range(0, attackClips.Length)];
        audioSource.Play();
    }
    public void PlayDeathSound()
    {
        audioSource.clip = dieClip;
        audioSource.Play();
    }
}
