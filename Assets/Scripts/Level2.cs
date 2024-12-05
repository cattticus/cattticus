using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;

public class Level2 : MonoBehaviour
{

    [SerializeField] GameObject gameOver;

    public Player player2; // Need reference to this object's rigid body
    
    public ParticleSystem explosion;

    public float respawnTime = 3.0f;

    public float respawnInvincibilityTime = 3.0f;

    public int lives = 3;

    public int score = 0;

    public TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText.text = "Score: " + score;
       
    }

    public void EnemyKilled(EnemyLvl2 enemy)
    {
        this.explosion.transform.position = enemy.transform.position;
        this.explosion.Play();

        if (enemy.size <= 0.75)
        {
            this.score += 200;
        }
        else if (enemy.size <= 1.0f)
        {
            this.score += 25;
        }
        else
        {
            this.score += 50;
        }

        scoreText.text = "Score: " + score;

        if(score >= 1000)
        {
            SceneManager.LoadScene("Level3");
        }
    }

    public void PlayerDied()
    {
        this.explosion.transform.position = this.player2.transform.position;
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
        
        Debug.Log("Player collided with enemy");
        

        player2.gameObject.SetActive(false); //wont run anything attatched to player until respawned

        FindObjectOfType<GameManager>().PlayerDied(); //will return ref to the game manager assuming it exists in the scene

    }
    private void Respawn()
    {
        player2.transform.position = Vector3.zero;
        player2.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        player2.gameObject.SetActive(true);

        Invoke(nameof(TurnOnCollisions), respawnInvincibilityTime);//makes invincible for a 3 seconds after respawning
    }

    private void TurnOnCollisions()
    {
        this.player2.gameObject.layer = LayerMask.NameToLayer("Player"); //makes player normal again
    }

    private void LoadLevel()
    {
        
    }
}
