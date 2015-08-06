using UnityEngine;
using System.Collections;

public class Reiniciar : MonoBehaviour {

	public GameObject pelota;
	public GameObject posicion;
	public InputEffects inputEffects;


	void OnTriggerEnter(Collider other){
		pelota.transform.position = posicion.transform.position;
		pelota.GetComponent<Rigidbody>().velocity = Vector3.zero;
		pelota.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		inputEffects.enabled = true;
	}
	
}
