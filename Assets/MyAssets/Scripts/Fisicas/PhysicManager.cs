using UnityEngine;
using System.Collections;

public class PhysicManager : MonoBehaviour {
	protected Rigidbody rb;
	public GameObject posicion;

	/*
	 * Modulo principal para la realizacion de las acciones de la IA
	 */
	virtual public void AccionIA(Vector3 direccion, float fuerza,int level){}


	/*
	 * Modulo principal para la recolocacion de los jugadores
	 */
	virtual public void reiniciar(){
		transform.position = posicion.transform.position;
		transform.rotation = posicion.transform.rotation;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		//rb.isKinematic = true;
	}

}

