using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantBackup : MonoBehaviour
{
    
    public int movementSpeed = 5;
    public int bulletSpeed = 100;
    public float fireRate = 2f;
    public float gunDistance = 2f;

    public float shootDistance = 5f;
    public float lookDistance = 10000;

    public Transform gunPosition;
    public GameObject destructionEffect;

    public GameObject bullet;

    public AudioClip hurtSound;

    bool faceLeft = false;

    GameManager _gameManager;
    Rigidbody2D _rigidbody2D;
    Transform player;
    AudioSource _audioSource;

    SpriteRenderer _renderer;
    Animator _animator;

    void Start() {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        _rigidbody2D = GetComponent<Rigidbody2D>();   

        player = GameObject.FindGameObjectWithTag("Player").transform;
        _audioSource = GetComponent<AudioSource>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        StartCoroutine(Follow());
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot() {
        while(true) {
            float distance = Vector2.Distance(transform.position, player.position);
            // Check if elephant can see player
            if(distance < lookDistance){
                // Shoot at player
                GameObject bulletInstance = Instantiate(bullet, gunPosition.position, gunPosition.rotation);   
                Vector2 angle_direction = (player.position - bulletInstance.transform.position);
                bulletInstance.GetComponent<Rigidbody2D>().AddForce(angle_direction.normalized * (bulletSpeed));
            }
            yield return new WaitForSeconds(fireRate);
        }
    }

    IEnumerator Follow(){
        while(true){
            yield return new WaitForSeconds(0.1f);
            float distance = Vector2.Distance(transform.position, player.position);
            // Check if within shooting distance
            if(distance < lookDistance && distance > shootDistance){
                // Move towards the player
                _renderer.flipX = faceLeft;
                Vector2 angle_direction = (player.position - transform.position);
                _rigidbody2D.velocity = angle_direction.normalized * movementSpeed;
            }
            else {
                _rigidbody2D.velocity = new Vector2(0, 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // Damage player if collided
        if (other.gameObject.CompareTag("Player")) {
            _gameManager.TakeDamage(1);
        }
    }

    void Update() {
        // Move the gun to aim at the player
        float yDiff = player.transform.position.y - transform.position.y;
        float xDiff = player.transform.position.x - transform.position.x;
        float angle = Mathf.Atan2(yDiff, xDiff);

        float gunX = Mathf.Cos(angle) * gunDistance + transform.position.x;
        float gunY = Mathf.Sin(angle) * gunDistance + transform.position.y;

        gunPosition.position = new Vector3(gunX, gunY, transform.position.z);
        gunPosition.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle);
        
        // Flip gun if needed
        bool left = (angle * Mathf.Rad2Deg + 270) % 360 < 180;
        if(faceLeft != left) {
            faceLeft = left;
            gunPosition.transform.localScale *= new Vector2(1, -1);
        }

        // Update animation
        _animator.SetBool("Moving", _rigidbody2D.velocity != new Vector2(0, 0));
    }
}
