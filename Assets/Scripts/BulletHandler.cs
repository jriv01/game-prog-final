using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour {
	private Rigidbody2D rb;
	public float speed;
	public float lifetime;  
	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
		float angle = transform.eulerAngles.z;
		float x = Mathf.Cos(angle*Mathf.PI/180);
		float y = Mathf.Sin(angle*Mathf.PI/180);

		Vector2 direction = transform.eulerAngles.y>0?new Vector2(-x,y):new Vector2(x,y);
		rb.velocity = direction * speed;
		Destroy(gameObject, lifetime);
	}
	private void Update() {
		//transform.Translate(Vector2.up*speed);

	}
    private void OnCollisionEnter2D(Collision2D col)
    { 
        Destroy(gameObject);
	}

}
