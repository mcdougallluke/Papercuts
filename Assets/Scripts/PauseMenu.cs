using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public PlaylistAudioPlayer playlistAudioPlayer;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(GameIsPaused)
            {
                Resume();
            }else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; 
        GameIsPaused = false;
        playlistAudioPlayer.GetComponent<AudioSource>().UnPause(); //Play music 
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; 
        GameIsPaused = true; 
        playlistAudioPlayer.GetComponent<AudioSource>().Pause(); //Pause music
    }
    
    public void loadMenu()
    {
        SceneManager.LoadScene(0);
    }
    
}
