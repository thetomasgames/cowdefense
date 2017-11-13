using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
	private Vector2 velocity;
	private bool flipped;

	public Animator ac;
	private bool alive;

	void Start ()
	{
	}

	public bool isFlipped ()
	{
		return this.flipped;
	}

	public void setFlipped (bool flipped)
	{
		this.flipped = flipped;
	}






}
