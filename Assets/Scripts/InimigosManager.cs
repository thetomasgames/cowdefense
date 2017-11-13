using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigosManager : MonoBehaviour
{

	public List<Transform> posicoes;
	public GameObject smallCarn;

	private System.Random rand;

	void Start ()
	{
		rand = new System.Random (System.Environment.TickCount);
		destroiFilhos ();
		InvokeRepeating ("instanciaSmallCarn", 1, 1);
	}

	private void destroiFilhos ()
	{
		foreach (Transform child in transform) {
			child.GetComponent<SmallCarn> ().die ();
		}
	}

	private void instanciaSmallCarn ()
	{
		GameObject.Instantiate (smallCarn,
			getPosicaoAleatoria (), Quaternion.identity, this.transform);
	}

	private Vector3 getPosicaoAleatoria ()
	{
		return  this.posicoes [rand.Next (0, posicoes.Count)].position;
	}
}
