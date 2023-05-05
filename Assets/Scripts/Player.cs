using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{   
    Rigidbody2D _rigidbody;
    public int speed = 10;
    public PlayerInput playerControls;
    public Weapon[] weapons;
    SpriteRenderer playerSprite;
    GameManager _gameManager;
    public AudioSource _audioSource;
    public List<string> foundWeapons = new List<string>();
    float nextFire = 0;
    
    bool faceLeft = false;
    Animator _animator;
    Weapon currentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        foundWeapons.Add("Handgun");
        _rigidbody = GetComponent<Rigidbody2D>();
        playerControls = GetComponent<PlayerInput>();
        playerSprite = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        _animator = GetComponent<Animator>();

        SetWeapon(PublicVars.currentWeapon);
    }

    // Update is called once per frame
    void Update()
    {   
        // Get the position of the gun
        Vector2 gunRot = playerControls.actions["Attack"].ReadValue<Vector2>();
        float angle = Mathf.Atan2(gunRot[1], gunRot[0]);

        float gunX = Mathf.Cos(angle) * currentWeapon.distanceFromPlayer + transform.position.x;
        float gunY = Mathf.Sin(angle) * currentWeapon.distanceFromPlayer + transform.position.y;

        Transform weaponPos = currentWeapon.weaponBody.transform;

        weaponPos.position = new Vector3(gunX, gunY, transform.position.z);
        weaponPos.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle);

        // Flip the gun if needed
        bool left = (angle * Mathf.Rad2Deg + 270) % 360 < 180;
        if(faceLeft != left) {
            faceLeft = left;
            weaponPos.transform.localScale *= new Vector2(1, -1);
        }

        // Fire the gun
        if (gunRot[0] != 0 || gunRot[1] != 0)
        {
            currentWeapon.weaponBody.SetActive(true);
            if(_gameManager.ammoCount > 0 && Time.time > nextFire)
            {
                if (currentWeapon.weaponName.Equals("Shotgun"))
                    _gameManager.addAmmo(-3);
                else
                    _gameManager.addAmmo(-1);
                nextFire = Time.time + currentWeapon.fireRate;
                if(currentWeapon.weaponName == "Shotgun") FireShotgun(weaponPos, angle);
                else FireGun(weaponPos, angle);
            }
        } else {
            currentWeapon.weaponBody.SetActive(false);
        }
    }
    void FireGun(Transform weaponPosition, float angle) {
        GameObject bulClone = Instantiate(currentWeapon.bulletPrefab, currentWeapon.bulletSpawn.position, weaponPosition.rotation);
        bulClone.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * currentWeapon.bulletSpeed);
    }

    void FireShotgun(Transform weaponPosition, float angle) {
        float spread = 20f * Mathf.Deg2Rad;
        int pelletCount = 1;
        for(int i = -pelletCount; i < pelletCount+1; i++) {
            float currAngle = angle + i * spread / (2 * pelletCount);
            GameObject bulClone = Instantiate(currentWeapon.bulletPrefab, currentWeapon.bulletSpawn.position, weaponPosition.rotation);
            bulClone.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(currAngle), Mathf.Sin(currAngle)) * currentWeapon.bulletSpeed);
        }
    }

    IEnumerator waitsec(int time, int inital_speed){
        yield return new WaitForSeconds(time);
        speed = inital_speed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("speedpickup")){
            int original_speed = speed;
            speed *= 2;
            StartCoroutine(waitsec(3,original_speed));
            Destroy(other.gameObject);
        }
    }

    public void SetWeapon(string weaponName) {
        foreach(Weapon weapon in weapons) {
            if(weapon.weaponName != weaponName) {
                weapon.weaponBody.SetActive(false);
            } else {
                currentWeapon = weapon;
                currentWeapon.weaponBody.SetActive(true);
            }
        }
        PublicVars.currentWeapon = weaponName;
    }
    public void changeWeapon()
    {
        for(int i = 0; i<foundWeapons.Count-1; i++)
        {
            if (foundWeapons[i] == currentWeapon.weaponName)
            {
                SetWeapon(foundWeapons[i + 1]);
                return;
            }
        }
        SetWeapon(foundWeapons[0]);
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
[System.Serializable]
public class Weapon {
    public string weaponName;
    public GameObject weaponBody;
    public Transform bulletSpawn;
    public GameObject bulletPrefab; 
    public float fireRate;
    public int bulletSpeed;
    public float distanceFromPlayer;
}
