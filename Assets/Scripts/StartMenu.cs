using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] GameObject settings;
     public void LoadGame()
    {
        SceneManager.LoadScene(1); //Main game scene
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Application has closed"); //double checking
    }

    public void Settings()
    {
        settings.SetActive(true);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
