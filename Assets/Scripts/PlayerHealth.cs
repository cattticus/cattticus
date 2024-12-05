using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 50;  
    [SerializeField] private int _currentHealth;
    [SerializeField] private GameManager _gameManager; // assign in inspector
    [SerializeField] GameObject gameOver;

    void Start()
    {
        _currentHealth = _maxHealth;
        Debug.Log("Our health is " + _currentHealth);
    }
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        Debug.Log("Our health is " + _currentHealth);

        if (_currentHealth <= 0)
        {
            _gameManager.PlayerDied();

            //gameOver.SetActive(true);
            gameOver = GameObject.Find("GameOver");

            gameOver.SetActive(true);

            // Here you'd have a reference to the game over screen. MAKE SURE TO CAP THE MINIMUM HEALTH AT ZERO AND RESET IT AFTER U DIE
            //dawg how does my ass do this pls im such an idiot

        }

    }
}
