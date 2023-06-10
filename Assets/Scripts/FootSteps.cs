using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public AudioSource audiosource;

    [Header("FootSteps source")]
    public AudioClip[] footstepsSound;

    private AudioClip GetRandomFootStep()
    {
        return footstepsSound[Random.Range(0, footstepsSound.Length)];
    }

    private void Step()
    {
        AudioClip clip = GetRandomFootStep(); 
        audiosource.PlayOneShot(clip);
    }


}

