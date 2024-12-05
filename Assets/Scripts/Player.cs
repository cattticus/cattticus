
using UnityEngine;

public class Player : MonoBehaviour
{
    public Projectile projectilePrefab;

    public float _thrustSpeed = 1.0f;
    public float _turnSpeed = 1.0f;

    private Rigidbody2D _rigidbody;
    private bool _thrusting; //bool for if certain keys are being pressed
    private float _turnDirection;

    private Transform _aimTransformFlipper;
    private Transform _aimTransform;

    private PlayerHealth _playerHealth;

    public Level2 PlayerDied;

    [SerializeField] private Animator _playerAnimator;

    private void Awake() //gets called once for life cycle of script
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerHealth = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        _aimTransformFlipper = transform.Find("AimTransformFlipper");
        _aimTransform = transform.Find("AimTransformFlipper/AimTransform");
    }

    private void Update() //checking for input

    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float verticalAxis = Input.GetAxisRaw("Vertical");
        Vector2 moveDirection = new Vector2(horizontalAxis, verticalAxis).normalized;

        transform.Translate(_thrustSpeed * Time.deltaTime * moveDirection);

        //_thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        //if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        //{
        //    _turnDirection = 1.0f;
        //}
        //else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        //{
        //    _turnDirection = -1.0f;
        //}
        //else
        //{
        //    _turnDirection = 0.0f;
        //}

        if (horizontalAxis > 0)
        {
            _playerAnimator.Play("slugRight");
            _aimTransformFlipper.localScale = new Vector3(-1, 0, 0);
        }
        else if (horizontalAxis < 0)
        {
            _playerAnimator.Play("slugLeft");
            _aimTransformFlipper.localScale = new Vector3(1, 0, 0);
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePosition.z = 0;
        _aimTransform.up = mousePosition - _aimTransform.position;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void FixedUpdate() //moving the player

    {
        //if (_thrusting)
        //{
        //    _rigidbody.AddForce(this.transform.up * this._thrustSpeed); //'up' if 2D 'forward' if 3D
        //}
        //if (_turnDirection != 0.0f)
        //{
        //    _rigidbody.AddTorque(_turnDirection * this._turnSpeed);
        //}
    }

    private void Shoot()
    {
        Projectile projectile = Instantiate(this.projectilePrefab, _aimTransform.position, _aimTransform.rotation);
        projectile.Project(_aimTransform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision2D) //handling player collision when hit
    {
        if (collision2D.gameObject.tag == "Enemy")
        {
            //Destroy(gameObject);

            Debug.Log("It's an enemememy!");
            if (_playerHealth != null)
            {
                Debug.Log("Wow we arent null wowoowoiieiie! uwu");
                _playerHealth.TakeDamage(10);
                Debug.Log("oooooo we took dmamamdmdgage!");
            }
        }

    }
}
