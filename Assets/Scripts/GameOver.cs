using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject gameOver;
    public void Setup()
    {
        gameOver.SetActive(true);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
