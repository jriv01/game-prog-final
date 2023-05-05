using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Raccoon : MonoBehaviour
{
    
    public int movementSpeed = 5;
    public float stealDistance = 1f;
    public float stealRate = 1f;
    public int stealMinimum = 1;
    public int stealMaximum = 3;
    public float lookDistance = 9.3f;

    bool faceLeft = false;
    public GameObject stealPrefab;


    GameManager _gameManager;
    Rigidbody2D _rigidbody2D;
    Transform player;
    AudioSource _audioSource;
    public AudioClip stealSound;
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
        StartCoroutine(Steal());
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

    IEnumerator Steal() {
        while(true){
            yield return new WaitForSeconds(stealRate);
            float distance = Vector2.Distance(transform.position, player.position);
            // Check if within attack distance
            if(distance <= stealDistance) {
                int amount = Random.Range(stealMinimum, stealMaximum+1);
                _gameManager.TakeMoney(amount);
                _audioSource.PlayOneShot(stealSound);
                // Play stealing effect
                // Set position of effect
                Vector3 slashPosition = new Vector3(
                    player.position.x,
                    player.position.y,
                    -5
                );
                // Instantiate the effect
                GameObject slash = Instantiate(stealPrefab, slashPosition, Quaternion.identity);
            }
        }
    }

    void Update() {
        // Update animation
        _animator.SetBool("Moving", _rigidbody2D.velocity != new Vector2(0, 0));
    }
}
