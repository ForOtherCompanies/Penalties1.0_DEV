using UnityEngine;
using System.Collections;

public class PelotaFisicas : MonoBehaviour {

	bool control = false;

	public Rigidbody rb;
	void Start() {
		rb = GetComponent<Rigidbody>();
	}
	void Update() {


		//TODO
		//añadir:
		//// poner el drag a 0.8 mientras este rodando por una superficie
		///  si no esta rodando ponemos el drag a 0
		///  si llega a un umbral minimo de rotacion (angularVelocity) la paramos

		if (Input.GetKeyDown("space")){
			rb.angularDrag = 0.8F;
			control = true;
			//Debug.Log ("frenando");
		}

		if (rb.angularVelocity.z < 1.5 && control){
			rb.angularDrag = 20f;
			//Debug.Log ("##PASA##");
		}



	}
}
