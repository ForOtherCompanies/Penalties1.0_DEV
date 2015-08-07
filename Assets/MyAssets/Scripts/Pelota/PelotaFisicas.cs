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
	public bool efectoIA = false;
	public Vector3 efecto;

	private float fuerzaEfecto;


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
			efecto = CalcularEfecto(rotationHelper.transform.eulerAngles.z);
			Debug.Log ("recibiendo efecto "+efecto);
			rb.AddForce (efecto, ForceMode.Force);

		}
		if (efectoIA) {
			rb.AddForce (Vector3.left *fuerzaEfecto, ForceMode.Force);
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

	public void LanzamientoIA(Vector3 direccion,float fuerza,int level){
		rb.isKinematic = false;
		rb.AddForce (direccion * fuerza*2);
		if (Random.Range (0, 100) < level * 9) {
			fuerzaEfecto = Random.Range(0,8);
			efectoIA= true;
		}
	}

	public void Lanzamiento(Vector3 direccion, float fuerza){
		rb.isKinematic = false;
		rb.AddForce (direccion * fuerza*2);
		recibiendoEfecto = true;
		Debug.Log ("LANZAMIENTO_EFECTO = "+ recibiendoEfecto);

	}

	Vector3 CalcularEfecto(float angle){
		float factor = 0;
	
		//si inclina a la izquierda
		if (angle > 0 && angle< 90){
			angle = Mathf.Clamp (angle, 0, 45); //clamp a 45, es decir 45º a derecha es el maximo que se registra para el efecto

		}

		//si inclina a derecha
		if (angle<360 && angle > 270){
			angle = Mathf.Clamp (angle, 315, 360); //clamp a 45, es decir 45º a derecha es el maximo que se registra para el efecto
			angle-=360;
		}

		factor = angle * maximoEfecto/45;
		//si no se inclina factor = 0 por lo que el temEfecto es (0,0,0)
		Vector3 tempEfecto = Vector3.left * factor;
		
		return tempEfecto;
	}

	public void OnCollisionEnter (Collision col){
		recibiendoEfecto = false;
	}


}
