using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SmallCarn : MonoBehaviour,Shootable
{
	private float speedMultiplier = 5;
	private float currentSpeed;
	public Rigidbody2D rb;
	public Animator ac;

	private bool flipped;
	private float targetVelocity;

	private GameManager manager;

	void Awake ()
	{
		manager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ();
		currentSpeed = speedMultiplier;
		System.Random rand = new  System.Random (System.Environment.TickCount);
		if (rand.NextDouble () > 0.5f) {
			changeDirection ();	
		}
	}

	void changeDirection ()
	{
		flipped = !flipped;
		transform.rotation = Quaternion.Euler (0, flipped ? 180 : 0, 0);
		currentSpeed = (flipped ? -1 : 1) * speedMultiplier;
	}

	void FixedUpdate ()
	{
		rb.velocity = new Vector2 (currentSpeed, rb.velocity.y);
		ac.SetFloat ("HorizontalSpeed", Mathf.Abs (rb.velocity.x));
	}

	void OnCollisionEnter2D (Collision2D c)
	{
		if (c.gameObject.CompareTag ("Obstacle")) {	//paredes cetas
			currentSpeed = 0;
			Invoke ("changeDirection", 0.5f);
		} else if (c.gameObject.CompareTag ("Player")) {
			manager.NotifyCowHurt ();
		}
	}

	public void getShot ()
	{
		die ();
	}

	public void die ()
	{
		manager.inimigoMorto ();
		ac.SetTrigger ("Death");
		rb.bodyType = RigidbodyType2D.Static;
		Destroy (this.gameObject, 3.0f);
	}
		
}
