using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class AudioSettings : MonoBehaviour
{
    public Slider musicVol, sfxVol; 
    public AudioMixer mainAudioMixer; 
    
    public void ChangeMusicVolume()
    {
        mainAudioMixer.SetFloat("MusicVol", musicVol.value);

    }
    public void ChangeSfxVolume()
    {
        mainAudioMixer.SetFloat("SFXVol", sfxVol.value);
        
    }
}
