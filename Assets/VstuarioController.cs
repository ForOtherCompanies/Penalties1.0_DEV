using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VstuarioController : MonoBehaviour
{
	public GameObject[] categorias;
	private GameObject[] catActual;
	private Vector3 posInicio;
	public GameObject[] panel;

	void Start ()
	{
		catActual = new GameObject[2];
		catActual [0] = categorias [0];
		catActual [1] = panel [0];
		posInicio = panel [0].GetComponent<RectTransform> ().localPosition;

	}

	public void EnableCat1 ()
	{
		catActual [0].SetActive (false);
		catActual [1].GetComponent<RectTransform> ().localPosition = posInicio;
		catActual [0] = categorias [0];
		catActual [1] = panel [0];
		catActual [0].SetActive (true);
	}

	public void EnableCat2 ()
	{
		catActual [0].SetActive (false);
		catActual [1].GetComponent<RectTransform> ().localPosition = posInicio;
		catActual [0] = categorias [1];
		catActual [1] = panel [1];
		catActual [0].SetActive (true);
	}

	public void exit ()
	{
		catActual [0].SetActive (false);
		catActual [1].GetComponent<RectTransform> ().localPosition = posInicio;
	}


}
