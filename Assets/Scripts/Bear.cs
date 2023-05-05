using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bear : MonoBehaviour
{

    enum State {FOLLOW, SHOOT, HEAL};
    
    public int movementSpeed = 6;
    public int maxHealth = 500;
    public int bulletSpeed = 100;
    public float fireRate = 2f;
    public float gunDistance = 2f;

    public float attackDistance = 5f;
    public float slashRate = 2f;

    public Transform healPosition;
    public GameObject healEffect;
    public GameObject slashEffect;
    public GameObject gunBody;
    public Transform bulletSpawn;
    public GameObject destructionEffect;

    public GameObject bulletPrefab;

    public AudioClip hurtSound;

    bool faceLeft = false;
    int health;

    GameManager _gameManager;
    Rigidbody2D _rigidbody2D;
    Transform player;
    AudioSource _audioSource;

    SpriteRenderer _renderer;
    Animator _animator;


    void Start() {
        StartCoroutine(LateStart());
    }

    IEnumerator LateStart() {
        yield return new WaitForSeconds(0);

        _gameManager = GameObject.FindObjectOfType<GameManager>();
        _rigidbody2D = GetComponent<Rigidbody2D>();   

        player = GameObject.FindGameObjectWithTag("Player").transform;
        _audioSource = GetComponent<AudioSource>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        health = maxHealth;

        StartCoroutine(Follow());
    }

    void ChangeState() {
        StopAllCoroutines();
        gunBody.SetActive(false);
        State currentState = (State)Random.Range(0, 3);
        switch (currentState) {
            case State.FOLLOW:
                StartCoroutine(Follow());
                break;
            case State.SHOOT:
                StartCoroutine(Shoot());
                break;
            case State.HEAL:
                StartCoroutine(Heal());
                break;
            default:
                break;
        }
    }

    
    IEnumerator Follow(){
        float time = 0;
        int followTime = 10;
        float nextSlash = 0;
        while(time < followTime){
            yield return new WaitForSeconds(0.1f);
            float distance = Vector2.Distance(transform.position, player.position);
            // Check if within shooting distance
            if(distance > attackDistance){
                // Move towards the player
                _renderer.flipX = faceLeft;
                Vector2 angle_direction = (player.position - transform.position);
                _rigidbody2D.velocity = angle_direction.normalized * movementSpeed;
            }
            else {
                _rigidbody2D.velocity = new Vector2(0, 0);
                if(Time.time > nextSlash) {
                    nextSlash = Time.time + slashRate;
                    _gameManager.TakeDamage(1);
                    Instantiate(slashEffect, player.position, Quaternion.identity);
                }
            }

            time += Time.deltaTime;
        }

        ChangeState();
    }

    IEnumerator Heal() {
        // How many times to heal and how long to heal for 
        int healCount = 3;
        int healAmount = 10;
        
        // 
        for(int i = 0; i < healCount; i++) {
            health += healAmount;
            if(healAmount > maxHealth) healAmount = maxHealth;
            Instantiate(healEffect, healPosition.position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }

        ChangeState();
    }

    IEnumerator Shoot() {
        gunBody.SetActive(true);
        int minFire = 3;
        int maxFire = 8;
        int fire = Random.Range(minFire, maxFire+1);

        float spread = 60f * Mathf.Deg2Rad;
        int pelletCount = 5;

        for(int i = 0; i < fire; i++) {
            float angle = GetGunAngle();

            for(int pellet = -pelletCount; pellet < pelletCount+1; pellet++) {
                float currAngle = angle + pellet * spread / (2 * pelletCount);
                GameObject bulClone = Instantiate(bulletPrefab, bulletSpawn.position, gunBody.transform.rotation);
                bulClone.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(currAngle), Mathf.Sin(currAngle)) * bulletSpeed);
            }
            yield return new WaitForSeconds(fireRate);
        }

        ChangeState();
    }

    float GetGunAngle() {
        Vector2 gunRot = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(gunRot[1], gunRot[0]);    
        return angle;
    }

    void UpdateGun() {
        float angle = GetGunAngle();
        float gunX = Mathf.Cos(angle) * gunDistance + transform.position.x;
        float gunY = Mathf.Sin(angle) * gunDistance + transform.position.y;

        gunBody.transform.position = new Vector3(gunX, gunY, transform.position.z);
        gunBody.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle);

        // Flip the gun if needed
        bool left = (angle * Mathf.Rad2Deg + 270) % 360 < 180;
        if(faceLeft != left) {
            faceLeft = left;
            gunBody.transform.transform.localScale *= new Vector2(1, -1);
        }
    }

    void Update() {
        if(gunBody.activeSelf) UpdateGun();

        // Update animation
        _animator.SetBool("Moving", _rigidbody2D.velocity != new Vector2(0, 0));

        if(health <= 0) {
            SceneManager.LoadScene("Win");
        }
    }
}
