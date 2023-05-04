using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raccoon : MonoBehaviour
{
    
    public int movementSpeed = 5;
    public float stealDistance = 1f;
    public float stealRate = 1f;
    public int stealMinimum = 1;
    public int stealMaximum = 3;
    bool faceLeft = false;
    public GameObject stealPrefab;


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
        StartCoroutine(Steal());
    }

    IEnumerator Follow(){
        while(true){
            yield return new WaitForSeconds(0.1f);
            float distance = Vector2.Distance(transform.position, player.position);
            // Check if within attack distance
            if(distance > stealDistance) {
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

    IEnumerator Steal() {
        while(true){
            yield return new WaitForSeconds(stealRate);
            float distance = Vector2.Distance(transform.position, player.position);
            // Check if within attack distance
            if(distance <= stealDistance) {
                int amount = Random.Range(stealMinimum, stealMaximum+1);
                _gameManager.TakeMoney(amount);

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
