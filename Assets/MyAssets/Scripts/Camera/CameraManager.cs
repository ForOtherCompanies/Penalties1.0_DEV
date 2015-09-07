using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	//keep public
	public GUIManager guiManager;
	public Camera [] camaras;
	//keep private
	private Camera camara;
	private Camera lastCamara;
	private Animator animacion;
	private int last;


	void Start(){
		camara = camaras [0];
		animacion = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void MoverEntrenamiento(){
		lastCamara = camara;
		camara = camaras [1];
		last = 1;
		animacion.SetBool ("Main", false);
		animacion.SetBool ("Entrenar", true);
	}

	public void MoverMultijugador(){
		lastCamara = camara;
		camara.enabled = false;
		camara = camaras [2];
		last = 2;
		animacion.SetBool ("Main", false);
		animacion.SetBool ("Multiplayer", true);
	}
	public void MoverVestuario(){
		lastCamara = camara;
		camara = camaras [3];
		last = 3;
		animacion.SetBool ("Main", false);
		animacion.SetBool ("Vestuario", true);
	}


	public void MoverInicio(){
		lastCamara = camara;
		camara = camaras [0];
		switch (last) {
		case 1:
			animacion.SetBool ("Entrenar", false);
			break;
		case 2:
			animacion.SetBool ("Multiplayer", false);
			break;
		case 3:
			animacion.SetBool ("Vestuario", false);
			break;
		}
		last = 0;
	}

	public void BackToMain(){
		animacion.SetBool ("Main", true);
	}


	public void activarCamara(){
		camara.enabled = true;
	}
	public void DesactivarCamara(){
		lastCamara.enabled = false;
	}
}
