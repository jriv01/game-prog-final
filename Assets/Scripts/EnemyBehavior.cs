using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyBehavior : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    GameManager _gameManager;

    public int movementSpeed = 5;

    public float distance = 10000;

    public GameObject destructionEffect;

    public AudioSource _audioSource;
    public AudioClip hurtSound;
    Transform user;
    // Animator _animator;

    void Start() {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        _rigidbody2D = GetComponent<Rigidbody2D>();   

        user = GameObject.FindGameObjectWithTag("Player").transform;
        //_animator = GetComponent<Animator>(); 
        _audioSource = GetComponent<AudioSource>();

        StartCoroutine(Follow());
    }


    IEnumerator Follow(){
        while(true){
            yield return new WaitForSeconds(0.1f);
            if(Vector2.Distance(transform.position, user.position) < distance && Vector2.Distance(transform.position, user.position) > 0){
                if (user.position.x > transform.position.x && transform.localScale.x < 0 || user.position.x < transform.position.x && transform.localScale.x > 0){
                    transform.localScale *= new Vector2(-1,1);
                }
                Vector2 angle_direction = (user.position - transform.position);
                _rigidbody2D.velocity = angle_direction.normalized * movementSpeed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bullet")) {
            //AudioSource.PlayClipAtPoint(destroyedSound, gameObject.transform.position);
            Instantiate(destructionEffect, transform.position, Quaternion.identity);
            //_gameManager.incrementEnemyScoreCounter(40);
            Destroy(other.gameObject);
            _audioSource.PlayOneShot(hurtSound);
            Destroy(gameObject);
        }
        
    }

    // Let enemy phase walls
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            AudioSource.PlayClipAtPoint(hurtSound, gameObject.transform.position);
            Instantiate(destructionEffect, transform.position, Quaternion.identity);
            //_gameManager.increaseEnemyScore(10);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Wall")){
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

}
