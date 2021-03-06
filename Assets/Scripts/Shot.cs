using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{

	public Animator ac;
	public Collider2D col;
	public Rigidbody2D rb;
	public float velocity = 5f;

	void Start ()
	{
		rb.velocity = transform.right * velocity;
	}

	public void OnCollisionEnter2D (Collision2D c)
	{
		if (c.collider.CompareTag ("Enemy")) {
			c.gameObject.GetComponent<Shootable> ().getShot ();

		}
		col.enabled = false;
		rb.bodyType = RigidbodyType2D.Kinematic;
		ac.SetTrigger ("Explode");
	}

	public void AnimationEnd ()
	{
		Destroy (this.gameObject);
	}

}
