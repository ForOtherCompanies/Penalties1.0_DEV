using UnityEngine;
using System.Collections;

public class PorteroFisicas : MonoBehaviour {

	// Use this for initialization
	public Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody>();
		rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Saltar(Vector3 direccion, float fuerza){
		rb.AddForce (direccion * fuerza*2);
	}
}
