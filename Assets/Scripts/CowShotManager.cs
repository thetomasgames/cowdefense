using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowShotManager : MonoBehaviour
{
	public ShotManager shotManager;
	public Transform shotPos;
	public Hero hero;
	public float periodoTiro = 1;

	private bool tiroDisponivel = true;

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown ("Fire1") && this.tiroDisponivel) {
			tiroDisponivel = false;
			hero.ac.SetTrigger ("Shoot");
			GameObject go = shotManager.GetCowBullet ();
			go.transform.rotation = hero.transform.rotation;
			go.transform.position = shotPos.position;
			Invoke ("habilitaTiro", 1);
		}
	}

	void habilitaTiro ()
	{
		this.tiroDisponivel = true;
	}
}
