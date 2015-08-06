using UnityEngine;
using System.Collections;

public class PelotaFisicas : MonoBehaviour {

	//references to external
	public GameObject rotationHelper;


	//references to components
	public Rigidbody rb;
	public float maximoEfecto = 8f;

	//set private
	public bool recibiendoEfecto = false;
	public Vector3 efecto;


	//debugOnly

	void Start() {
		rb = GetComponent<Rigidbody>();
		rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
		rb.isKinematic = true;
		efecto = Vector3.zero;
	}
	void Update() {



		//if (rb.isKinematic==false)
			//Debug.Log ("UPDATE_EFECTO = "+ recibiendoEfecto);
		if (recibiendoEfecto){

			efecto = CalcularEfecto();
			Debug.Log ("recibiendo efecto "+efecto);
			rb.AddForce (efecto, ForceMode.Force);

		}



		//TODO
		//añadir:
		//// poner el drag a 0.8 mientras este rodando por una superficie
		///  si no esta rodando ponemos el drag a 0
		///  si llega a un umbral minimo de rotacion (anoh gularVelocity) la paramos
/*
		if (Input.GetKeyDown("space")){
			rb.angularDrag = 0.8F;
			control = true;
			//Debug.Log ("frenando");
		}

		if (rb.angularVelocity.z < 1.5 && control){
			rb.angularDrag = 20f;
			//Debug.Log ("##PASA##");
		}
*/
	}

	public void Lanzaminento(Vector3 direccion, float fuerza){
		rb.isKinematic = false;
		rb.AddForce (direccion * fuerza*2);
		recibiendoEfecto = true;
		Debug.Log ("LANZAMIENTO_EFECTO = "+ recibiendoEfecto);

	}

	Vector3 CalcularEfecto(){
		float factor;

		float angle = rotationHelper.transform.eulerAngles.z;
		//si inclina a la izquierda
		if (rotationHelper.transform.eulerAngles.z > 0 && rotationHelper.transform.eulerAngles.z < 90){
			if (angle > 100)
				angle = Mathf.Clamp (angle, 0, 45); //clamp a 45, es decir 45º a derecha es el maximo que se registra para el efecto

			factor = angle * maximoEfecto/45;
			Vector3 tempEfecto = Vector3.left * factor;

			return tempEfecto;
		}


		//si inclina a derecha
		if (rotationHelper.transform.eulerAngles.z <360 && rotationHelper.transform.eulerAngles.z > 270){
			if (angle > 280)
				angle = Mathf.Clamp (angle, 360, 315); //clamp a 45, es decir 45º a derecha es el maximo que se registra para el efecto

			angle = Mathf.Abs (360-angle);
			factor = angle * maximoEfecto/45;
			Vector3 tempEfecto = -Vector3.left * factor;
			
			return tempEfecto;
		}

		return Vector3.zero;
	}

	public void OnCollisionEnter (Collision col){
		recibiendoEfecto = false;
	}


}
