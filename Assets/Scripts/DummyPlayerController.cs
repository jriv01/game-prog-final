using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DummyPlayerController : MonoBehaviour
{   
    Rigidbody2D _rigidbody;
    public int speed = 10;
    public int bulletSpeed = 10;

    public Sprite shotgunSprite;
    public PlayerInput playerControls;
    public Sprite[] spriteArray;
    float currTime = 0f;
    float next = 0.5f;
    public SpriteRenderer playerSprite;
    GameManager _gameManager;
    public AudioClip moneypickup;
    public AudioClip shotgunPickup;
    AudioSource _audioSource;

    int currSprite = 0;
    public GameObject gun;
    Rigidbody2D gunBody;
    public GameObject bullet;
    public float nextFire = 0;
    public float fireRate;
    public Transform shotSpawn;
    public GameObject starterShot;
    public GameObject gunSprite;
    GameObject gunHolding;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        playerControls = GetComponent<PlayerInput>();
        playerSprite = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        gunBody = gun.GetComponent<Rigidbody2D>();
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        gunHolding = GameObject.FindGameObjectWithTag("GunHolding");


    }

    // Update is called once per frame
    void Update()
    {
        Vector2 gunRot = playerControls.actions["Attack"].ReadValue<Vector2>();
        float gunAngle = Mathf.Atan2(gunRot[0], gunRot[1]) * Mathf.Rad2Deg;
        /*if (gunAngle > 0)
        {
            gunSprite.GetComponent<SpriteRenderer>().flipY = true;
            
        }
        else
        {
            gunSprite.GetComponent<SpriteRenderer>().flipY = false;
        }*/

        if (gunRot[0] != 0 || gunRot[1] != 0)
        {
            gunBody.rotation = -gunAngle - 90;
            if(Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                GameObject bulClone = Instantiate(starterShot, shotSpawn.position + new Vector3(0,0,0), shotSpawn.rotation) as GameObject;
            }
        }

        else
            gunBody.rotation = 0;
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
            gunHolding.GetComponent<SpriteRenderer>().sprite = shotgunSprite;
            // gunHolding.transform.position = new Vector2(-1.642f,2.4f); 
            gunHolding.transform.localScale = new Vector2(38.1f,41.8f);
            fireRate = 0.5f;
            
            //play sound effect
        }
        // if (col.CompareTag("Grenade")) {
        //     _gameManager.UnlockGrenade();
        //     _audioSource.PlayOneShot(moneypickup);  
        //     Destroy(col.gameObject);
        //     //play sound effect
        // }
    }

    void FixedUpdate(){
        Vector2 move = playerControls.actions["Move"].ReadValue<Vector2>();
        //Debug.Log(move);
        _rigidbody.velocity = move * speed;
        if(move[0]> 0)
        {
            playerSprite.flipX = true;
        }
        if(move[0] < 0)
        {
            playerSprite.flipX = false;
        }

        currTime += Time.deltaTime;
        if (move[0] + move[1] != 0 && currTime >= next)
        {
            currTime = 0;

            if (currSprite == 1)
                currSprite = 2;
            else
                currSprite = 1;
        }
        else
            currSprite = 0;
        playerSprite.sprite = spriteArray[currSprite];
    }
}
