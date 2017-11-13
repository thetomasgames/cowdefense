using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotManager : MonoBehaviour
{
	public GameObject shotCowPrefab;


	public GameObject GetCowBullet ()
	{
		return GameObject.Instantiate (shotCowPrefab);
	}
}
