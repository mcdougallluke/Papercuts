using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public void RestartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {
        SceneManager.LoadScene(0);
    }
}
