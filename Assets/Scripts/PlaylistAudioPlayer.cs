using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaylistAudioPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private List<AudioClip> playlist = new List<AudioClip>();
    private int currentTrackIndex = 0;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (playlist.Count > 0)
        {
            PlayCurrentTrack();
        }
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextTrack();
        }
    }

    private void PlayCurrentTrack()
    {
        audioSource.clip = playlist[currentTrackIndex];
        audioSource.Play();
    }

    private void PlayNextTrack()
    {
        currentTrackIndex++;
        if (currentTrackIndex >= playlist.Count)
        {
            currentTrackIndex = 0; // Loop back to the start of the playlist
        }
        PlayCurrentTrack();
    }

    // Optional: Function to add more clips to the playlist
    public void AddClipToPlaylist(AudioClip clip)
    {
        playlist.Add(clip);
    }

    // Optional: Function to remove a clip from the playlist
    public void RemoveClipFromPlaylist(AudioClip clip)
    {
        playlist.Remove(clip);
    }
}

