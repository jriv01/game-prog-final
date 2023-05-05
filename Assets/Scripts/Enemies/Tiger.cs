using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tiger : MonoBehaviour
{
    
    public int movementSpeed = 5;
    public float attackDistance = 3f;
    public float attackRate = 0.5f;
    public int attackDamage = 1;
    public float lookDistance = 9.3f;
    public GameObject slashPrefab;
    bool faceLeft = false;

    GameManager _gameManager;
    Rigidbody2D _rigidbody2D;
    Transform player;
    AudioSource _audioSource;

    public AudioClip slashSound;

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
        StartCoroutine(Attack());
    }

    IEnumerator Follow(){
        while(true){
            yield return new WaitForSeconds(0.1f);
            float distance = Vector2.Distance(transform.position, player.position);
            // Check if within attack distance
            if(distance < lookDistance) {
                // Move towards the player
                _renderer.flipX = faceLeft;
                agent.SetDestination(player.position);
            }
            else {
                agent.SetDestination(transform.position);
            }
        }
    }

    IEnumerator Attack() {
        while(true){
            yield return new WaitForSeconds(attackRate);
            float distance = Vector2.Distance(transform.position, player.position);
                print("tiger "+distance);

            // Check if within attack distance
            if(distance <= attackDistance) {
                print("nice");
                _gameManager.TakeDamage(attackDamage);
                _audioSource.PlayOneShot(slashSound);
                // Play slashing effect
                // Set position of effect
                Vector3 slashPosition = new Vector3(
                    player.position.x,
                    player.position.y,
                    -5
                );
                // Instantiate the effect and randomly flip it
                GameObject slash = Instantiate(slashPrefab, slashPosition, Quaternion.identity);
                if(Random.Range(0, 2) == 0) {
                    slash.GetComponent<SpriteRenderer>().flipX = true;
                }
            }
        }
    }

    void Update() {
        // Update animation
        _animator.SetBool("Moving", _rigidbody2D.velocity != new Vector2(0, 0));
    }
}
