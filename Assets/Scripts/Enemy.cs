using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    public int speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(ChasePlayer());
    }
    IEnumerator ChasePlayer(){
        while (true) {
            print("wah");
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
            yield return new WaitForSeconds(0.1f);
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
