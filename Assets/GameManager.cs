using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	Text livesText;
	public Text info;
	public Animator ac;
	public GameObject heart;
	public Transform heartsTransform;

	public bool playing;
	private int lives;
	private int inimigosMortos;
	private float timeSinceStart;
	private bool invencible;

	void Start ()
	{
		inimigosMortos = 0;
		invencible = false;
		timeSinceStart = 0;
		lives = 3;
		playing = true;
		updateAliveAnim ();
	}

	void Update ()
	{
		timeSinceStart += Time.deltaTime;
		updateUI ();
	}

	public void NotifyCowHurt ()
	{
		if (!invencible) {
			invencible = true;
			Invoke ("setInvencibleFalse", 1);
			lives--;
			if (lives == 0) {
				gameOver ();
			}
			updateUI ();
		}
	}

	private void setInvencibleFalse ()
	{
		invencible = false;
	}

	private void updateUI ()
	{
		info.text = inimigosMortos + " inimigos mortos \n" + Mathf.Round (timeSinceStart) + " segundos\n" + lives;
		if (heartsTransform.childCount != lives) {
			foreach (Transform child in heartsTransform) {
				GameObject.Destroy (child.gameObject);
			}
			for (int i = 0; i < lives; i++) {
				GameObject.Instantiate (heart, heartsTransform);
			}
		}
	}

	private void gameOver ()
	{
		playing = false;
		updateAliveAnim ();
		Invoke ("Start", 3);	
	}

	public void inimigoMorto ()
	{
		inimigosMortos++;
		updateUI ();
	}

	private void updateAliveAnim ()
	{
		ac.SetBool ("Alive", playing);
	}
}
