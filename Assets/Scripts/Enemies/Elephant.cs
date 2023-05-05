using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Elephant : MonoBehaviour
{
    
    public int movementSpeed = 5;
    public int bulletSpeed = 100;
    public float fireRate = 2f;
    public float gunDistance = 2f;

    public float shootDistance = 8f;
    public float lookDistance = 9.3f;

    public Transform gunPosition;
    public GameObject destructionEffect;

    public GameObject bullet;

    public AudioClip hurtSound;

    public AudioClip shootSound;

    bool faceLeft = false;

    GameManager _gameManager;
    Rigidbody2D _rigidbody2D;
    Transform player;
    AudioSource _audioSource;

    SpriteRenderer _renderer;
    Animator _animator;
    NavMeshAgent agent;

    void OnEnable() {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

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
            if(distance <= shootDistance){
                // Shoot at player
                GameObject bulletInstance = Instantiate(bullet, gunPosition.position, gunPosition.rotation);   
                Vector2 angle_direction = (player.position - bulletInstance.transform.position);
                bulletInstance.GetComponent<Rigidbody2D>().AddForce(angle_direction.normalized * (bulletSpeed));
                _audioSource.PlayOneShot(shootSound);
            }
            yield return new WaitForSeconds(fireRate);
        }
    }

    IEnumerator Follow(){
        while(true){
            yield return new WaitForSeconds(0.1f);
            float distance = Vector2.Distance(transform.position, player.position);
            print(distance);
            // Check if within shooting distance
            if(distance < lookDistance){
                // Move towards the player
                _renderer.flipX = faceLeft;
                agent.SetDestination(player.position);
            }
            else {
                agent.SetDestination(transform.position);
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
