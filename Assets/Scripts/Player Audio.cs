using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : AudioPlayer
{
    [SerializeField]
    private AudioClip pickUpPaperClip = null;

    [SerializeField]
    private AudioClip takeDamageClip = null;

    public void PlayPickUpSound()
    {
        PlayClip(pickUpPaperClip);
    }

    public void PlayHitSound()
    {
        PlayClip(takeDamageClip);
    }
}
