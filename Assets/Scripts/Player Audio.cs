using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : AudioPlayer
{
    [SerializeField]
    private AudioClip pickUpPaperClip = null;

    public void PlayPickUpSound()
    {
        PlayClip(pickUpPaperClip);
    }
}
