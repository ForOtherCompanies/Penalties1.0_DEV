using UnityEngine;
using System.Collections;

public class PorteroFisicas : PhysicManager{

	// Use this for initialization


	void Start() {
		rb = GetComponent<Rigidbody>();
		rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        GameConector.Instance.sendPortero(transform.position, transform.rotation);
	
	}
	public override void AccionIA(Vector3 direccion, float fuerza,int level){
		rb.AddForce (direccion * fuerza*2);
	}


    internal void Saltar(Vector3 final)
    {
        Vector3 direccionSalto; 
        float fuerzaSalto;

        Vector3 vectorSalto = final-this.transform.position;

        //si la potencia del salto es demasiado grande se clampea a 150
        if (vectorSalto.magnitude > 150)
            vectorSalto = Vector3.ClampMagnitude(vectorSalto, 150);

        //partimos el vector en direccion+magnitud para mandarselo al script de fisicas del portero
        fuerzaSalto = vectorSalto.magnitude;
        direccionSalto = vectorSalto.normalized;
        direccionSalto.x *= (-1);
        rb.AddForce(direccionSalto * fuerzaSalto * 2);
    }

    internal void setPosition(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }
}
