using UnityEngine;
using System.Collections;

public class VstuarioController : MonoBehaviour {
	public GameObject[] categorias;
	private GameObject catActual;

	void Start(){
		catActual = categorias [0];
	}
	public void EnableCat1(){
		catActual.SetActive (false);
		catActual = categorias [0];
		catActual.SetActive (true);
	}
	public void EnableCat2(){
		catActual.SetActive (false);
		catActual = categorias [1];
		catActual.SetActive (true);
	}
	



}
