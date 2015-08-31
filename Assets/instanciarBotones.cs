using UnityEngine;
using System.Collections;

public class instanciarBotones : MonoBehaviour {
	public int numBotones;
	public GameObject posicionInicio;
	public GameObject posicionDer;
	public GameObject posicionAbajo;
	public GameObject boton;
	private float incrementoDer;
	private float incrementoDown;
	public int numBotFila;
	private Vector3 posicionActual;
	public GameObject[] botonClon;
	public RectTransform rectTrans;
	public Vector3[] esquinasPanel;



	// Use this for initialization
	void Start () {
		esquinasPanel = new Vector3[4];
		botonClon = new GameObject[numBotones];
		rectTrans.GetWorldCorners (esquinasPanel);
		incrementoDer = posicionDer.transform.position.y - posicionInicio.transform.position.y;
		incrementoDown = posicionAbajo.transform.position.x - posicionInicio.transform.position.x;
		posicionActual = posicionInicio.transform.position;
		float ancho = esquinasPanel [0].y - esquinasPanel [1].y;
		float aux;
		aux = ancho / incrementoDer;
		numBotFila = Mathf.FloorToInt (aux);
		InstanciarBotonesVestuario ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void InstanciarBotonesVestuario(){
		for (int i = 0; i< numBotones; i++) {
			botonClon[i] = (GameObject)Instantiate (boton, posicionActual, transform.rotation);
			ActualizarPosicion(i);
		}
	}
	void ActualizarPosicion(int i){
		if (i % numBotFila == numBotFila - 1) {
			posicionActual = botonClon[i-numBotFila-1].transform.position;
			posicionActual.x+= incrementoDown;
		} else {
			posicionActual.y+=incrementoDer;
		}
	}
}
