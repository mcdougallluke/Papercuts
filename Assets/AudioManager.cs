using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]AudioSource musicSource;
    [SerializeField]AudioSource sfxSource;

    public AudioClip Background; 
    public AudioClip soundEffect; 

    private void start()
    {
        musicSource.clip = Background;
        musicSource.Play();
    }

    private void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    

}
