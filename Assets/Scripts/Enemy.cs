using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Sprite[] sprites;

    public float size = 1.0f;

    public float minSize = 0.7f;

    public float maxSize = 1.7f;

    public float speed = 50.0f;

    public float maxLifetime = 30.0f;

    private SpriteRenderer _spriteRenderer;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();

    }
    private void Start()
    {
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)]; //picks a random enemy to spawn
        this.transform.localScale = Vector3.one * this.size; //chooses a random size for my enemies

        _rigidbody.mass = this.size; //assigns mass based on size chosen
    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            if ((this.size * 0.5f) >= this.minSize) //splits enemy by half its size if hit
            {
                SplitEnemy();
                SplitEnemy();
            }
            Destroy(this.gameObject); //destroys enemy on split
        }

        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject, 0.5f); //destroys enemy 0.5 seconds after collission with player
        }

        FindObjectOfType<GameManager>().EnemyDestroyed(this);
        Destroy(this.gameObject);
    }

    private void SplitEnemy() // function to split enemy
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f; //adds slight offset to enemy spawn when split (makes it look more natural)

        Enemy half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;

        half.SetTrajectory(Random.insideUnitCircle.normalized * this.speed); //gives random trajectory to split enemies

    }
}

