using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiger : MonoBehaviour
{
    
    public int movementSpeed = 5;
    public float attackDistance = 1f;
    public float attackRate = 0.5f;
    public int attackDamage = 1;
    public GameObject slashPrefab;
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
        StartCoroutine(Attack());
    }

    IEnumerator Follow(){
        while(true){
            yield return new WaitForSeconds(0.1f);
            float distance = Vector2.Distance(transform.position, player.position);
            // Check if within attack distance
            if(distance > attackDistance) {
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

    IEnumerator Attack() {
        while(true){
            yield return new WaitForSeconds(attackRate);
            float distance = Vector2.Distance(transform.position, player.position);
            // Check if within attack distance
            if(distance <= attackDistance) {
                _gameManager.TakeDamage(attackDamage);
                
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
