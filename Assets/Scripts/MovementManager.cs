using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
	private bool rising;
	private float timeJumpStart;
	public float risingTime;
	private bool grounded;
	public float force = 150.0f;

	private float velocidadeEscada = 2f;

	public Rigidbody2D rb;
	public GameManager manager;
	public Hero hero;

	private bool podeSubirEscada = false;
	private bool segurandoNaEscada;


	void Update ()
	{
		if (manager.playing) {
			managePulo ();
			manageMovimentacao ();
			manageEscada ();
			setAnimatorValues ();
		}
	}

	private void manageMovimentacao ()
	{
		Vector2 newVelocity = new Vector2 (Input.GetAxis ("Horizontal") * 10, rb.velocity.y);
		if (newVelocity.x != 0) {
			if ((hero.isFlipped () && newVelocity.x > 0) || (!hero.isFlipped () && newVelocity.x < 0)) {
				hero.ac.SetTrigger ("Turn");
			}
			hero.setFlipped (newVelocity.x < 0);
			transform.rotation = Quaternion.Euler (0, hero.isFlipped () ? 180 : 0, 0);
		}
		rb.velocity = newVelocity;
	}

	private void managePulo ()
	{
		if (Input.GetButtonDown ("Jump")) {
			if (grounded) {
				timeJumpStart = Time.time;
				addForce (20);
				rising = true;
			}
		} else if (Input.GetButton ("Jump")) {
			if (rising) {
				addForce ();
			}
		} else if (Input.GetButton ("Jump")) {
			rising = false;
		}

		if ((Time.time - timeJumpStart) > risingTime) {
			rising = false;
		}
	}

	private void manageEscada ()
	{
		if (!segurandoNaEscada && Input.GetAxis ("Vertical") > 0 && podeSubirEscada) {
			segurandoNaEscada = true;
		}
		if (segurandoNaEscada) {
			rb.gravityScale = 0;
			rb.velocity = new Vector2 (rb.velocity.x, Input.GetAxis ("Vertical") * velocidadeEscada);
		} else {
			rb.gravityScale = 2;
		}

	}

	private void setAnimatorValues ()
	{
		hero.ac.SetFloat ("HorizontalSpeed", Mathf.Abs (rb.velocity.x));
		hero.ac.SetFloat ("VerticalSpeed", rb.velocity.y);
		hero.ac.SetFloat ("VerticalSpeedAbs", Mathf.Abs (rb.velocity.y));
		hero.ac.SetBool ("SegurandoEscada", segurandoNaEscada);
	}

	private void addForce (float multiplier = 1f)
	{
		rb.AddForce (Vector2.up * force * multiplier);
	}

	public void OnCollisionEnter2D (Collision2D c)
	{

		if (manager.playing) {
			if (c.collider.CompareTag ("Enemy")) {
				manager.NotifyCowHurt ();
				hero.ac.SetTrigger ("Death");
			} else if (isGround (c)) {
				grounded = true;
				hero.ac.SetTrigger ("Land");
			}
		}
	}

	public void OnCollisionExit2D (Collision2D c)
	{
		if (isGround (c)) {
			grounded = false;
			hero.ac.SetTrigger ("Jump");
			hero.ac.ResetTrigger ("Land");
		}

	}

	public void OnTriggerEnter2D (Collider2D c)
	{
		if (c.gameObject.CompareTag ("Escada")) {
			podeSubirEscada = true;
		}
	}

	public void OnTriggerExit2D (Collider2D c)
	{
		if (c.gameObject.CompareTag ("Escada")) {
			podeSubirEscada = false;
			segurandoNaEscada = false;
		}
	}

	private bool isGround (Collision2D c)
	{
		return c.collider.CompareTag ("Ground") || c.collider.CompareTag ("Obstacle");
	}



}
