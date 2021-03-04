using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//sound generated every time we swing the axe.
public class AxeSwingSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] swingSound;

    void PlaySwingSound()
    {
        audioSource.clip = swingSound[Random.Range(0, swingSound.Length)];
        audioSource.Play();
    }
   
}
