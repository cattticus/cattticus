using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player; // Set in inspector
    private PlayerHealth playerHealth;

    private ParticleSystem explosion;

    private float respawnTime = 3.0f;

    private float respawnInvincibilityTime = 3.0f;

    private int lives = 3;

    private int score = 0;

    // Three levels of scoring based on the sizes of the enemies. Set in inspector based on what level you're in
    [SerializeField] private int _lowScore;
    [SerializeField] private int _mediumScore;
    [SerializeField] private int _highestScore;

    [SerializeField ]private TextMeshProUGUI scoreText;

    [SerializeField] GameObject gameOver;


    private void Start()
    {
        scoreText.text = "Score: " + score;

        playerHealth = player.gameObject.GetComponent<PlayerHealth>(); // This gets the PlayerHealth script from the same player object
    }

    public void EnemyDestroyed(Enemy enemy)
    {
       // Debug.Log("Ch");
       // this.explosion.transform.position = enemy.transform.position;
       // this.explosion.Play();

        //increasing score based on size of enemy (smaller the enemy, the more points you get)
        if(enemy.size < 0.75)
        {
            this.score += _highestScore;
        }
        else if (enemy.size <= 1.0f)
        {
            this.score += _mediumScore;
        }
        else
        {
            this.score += _lowScore;
        }

        scoreText.text = "Score: " + score;

        if (this.score >= 1000)
        {
            SceneManager.LoadScene("Level2");
        }
    }

    // Need to reference specifically this function for the player's health
    public void PlayerDied()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();
       
        this.lives--; //decreases lives by 1


        if (this.lives >= 2)
        {
            this.score -= 100;
        }



        if (this.lives <= 0) //handling 'game over' state
        {
            gameOver.SetActive(true);
        }
        else
        {
           Invoke(nameof(Respawn), this.respawnTime); 
        }

        PlayerDied();

    }

    private void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions"); 
        this.player.gameObject.SetActive(true);
       
        this.Invoke(nameof(TurnOnCollisions), this.respawnInvincibilityTime);//makes invincible for 3 seconds after respawning
    }

    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player"); //makes player normal again
    }

    private void Level2()
    {
       
    }

    private void GameOver()
    {
       

    }
}
