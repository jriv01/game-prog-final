using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{   
    Rigidbody2D _rigidbody;
    public int speed = 10;
    public int bulletSpeed = 1000;

    public Sprite shotgunSprite;
    public PlayerInput playerControls;
    SpriteRenderer playerSprite;
    GameManager _gameManager;
    public AudioClip moneypickup;
    public AudioClip shotgunPickup;
    AudioSource _audioSource;

    public GameObject bulletPrefab;
    float nextFire = 0;
    public float fireRate;
    public GameObject gun;
    public Transform bulletSpawn;
    
    bool faceLeft = false;
    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        playerControls = GetComponent<PlayerInput>();
        playerSprite = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        // Get the position of the gun
        int gunDistance = 1;
        Vector2 gunRot = playerControls.actions["Attack"].ReadValue<Vector2>();
        float angle = Mathf.Atan2(gunRot[1], gunRot[0]);

        float gunX = Mathf.Cos(angle) * gunDistance + transform.position.x;
        float gunY = Mathf.Sin(angle) * gunDistance + transform.position.y;

        gun.transform.position = new Vector3(gunX, gunY, transform.position.z);
        gun.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle);

        // Flip the gun if needed
        bool left = (angle * Mathf.Rad2Deg + 270) % 360 < 180;
        if(faceLeft != left) {
            faceLeft = left;
            gun.transform.transform.localScale *= new Vector2(1, -1);
        }

        // Fire the gun
        if (gunRot[0] != 0 || gunRot[1] != 0)
        {
            if(Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                GameObject bulClone = Instantiate(bulletPrefab, bulletSpawn.position, gun.transform.rotation);
                bulClone.GetComponent<Rigidbody2D>().AddForce(new Vector2(gunRot[0], gunRot[1]).normalized * bulletSpeed);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Money")) {
            _gameManager.AddMoney(10);
            _audioSource.PlayOneShot(moneypickup);  
            Destroy(col.gameObject);
            //play sound effect
        }
        if (col.CompareTag("Shotgun")) {
            _audioSource.PlayOneShot(shotgunPickup);  
            Destroy(col.gameObject);
            gun.GetComponent<SpriteRenderer>().sprite = shotgunSprite;
            gun.transform.localScale = new Vector2(15f, 15f);
            fireRate = 0.5f;
        }
        else if (col.CompareTag("healthpickup")){
            _gameManager.incrementHealthCounter(1);
            Destroy(col.gameObject);
        
        }
    }

    void FixedUpdate(){
        // Update movement
        Vector2 move = playerControls.actions["Move"].ReadValue<Vector2>();
        _rigidbody.velocity = move * speed;
        if(move[0] > 0)
        {
            playerSprite.flipX = true;
        }
        else if(move[0] < 0)
        {
            playerSprite.flipX = false;
        }

        // Update animator
        _animator.SetBool("Moving", _rigidbody.velocity != new Vector2(0, 0));
    }
}
