using UnityEngine;
using System.Collections;

public class PorteroFisicas : PhysicManager{

	// Use this for initialization


	void Start() {
		rb = GetComponent<Rigidbody>();
		rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public override void AccionIA(Vector3 direccion, float fuerza,int level){
		rb.AddForce (direccion * fuerza*2);
	}
	public void Saltar(Vector3 direccion, float fuerza){
		rb.AddForce (direccion * fuerza*2);
	}

}
