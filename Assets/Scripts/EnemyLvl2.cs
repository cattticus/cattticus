using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyLvl2 : MonoBehaviour
{
    public static event Action<EnemyLvl2> OnEnemyKilled;

    [SerializeField] float moveSpeed = 5f;
    Rigidbody2D rb;
    Transform playerTarget;
    Vector2 moveDirection;
    public float size = 0f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTarget = GameObject.Find("Player").transform; //finds player to track (duh)
    }


    private void Update()
    {
     if (playerTarget)
        {
            Vector3 direction = (playerTarget.position - transform.position).normalized; //gives direction of where needed to go
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        if (playerTarget)
        {
            rb.velocity = new Vector2 (moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }
    private void Start()
    {
        this.transform.localScale = Vector3.one * this.size;
        rb.mass = this.size;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Destroy(this.gameObject);//destroys on contact with projectile
        }

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Enemy collided with player");
            Destroy(this.gameObject); 
        }

        FindObjectOfType<Level2>().EnemyKilled(this);
        Destroy(this.gameObject);

    }

   

    /*{
        Debug.Log($"Damage amount: {damageAmount}");
        enemyHealth -= damageAmount;
        Debug.Log($"Health is now: {enemyHealth}");

        if ( enemyHealth < 0 )
        {
            Destroy(gameObject);
            OnEnemyKilled?.Invoke(this);
        }
    } */
}
