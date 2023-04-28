using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DummyPlayerController : MonoBehaviour
{   
    Rigidbody2D _rigidbody;
    public int speed = 10;
    public PlayerInput playerControls;
    public Sprite[] spriteArray;
    float currTime = 0f;
    float next = 0.5f;
    public SpriteRenderer playerSprite;
    GameManager _gameManager;

    int currSprite = 0;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        playerControls = GetComponent<PlayerInput>();
        playerSprite = GetComponent<SpriteRenderer>();
        _gameManager = GameObject.FindObjectOfType<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Money")) {
            _gameManager.AddMoney(10);
            Destroy(gameObject);
            //play sound effect
        }
    }

    void FixedUpdate(){
        Vector2 move = playerControls.actions["Move"].ReadValue<Vector2>();
        Debug.Log(move);
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
