using UnityEngine;
using System.Collections;
using GooglePlayGames;

public class PorteroFisicas : PhysicManager
{

    // Use this for initialization


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    // Update is called once per frame
    public void SendInfo(Vector3 final)
    {
        GameConector.sendPortero(final);

    }
    public override void AccionIA(Vector3 direccion, float fuerza, int level)
    {
        rb.AddForce(direccion * fuerza * 2);
    }


    internal void Saltar(Vector3 final)
    {/*
        Vector3 direccionSalto;
        float fuerzaSalto;

        Vector3 vectorSalto = final - this.transform.position;

        //si la potencia del salto es demasiado grande se clampea a 150
        vectorSalto.z = 0;
        fuerzaSalto = Vector3.ClampMagnitude(vectorSalto, 150).magnitude;

        //partimos el vector en direccion+magnitud para mandarselo al script de fisicas del portero
        //fuerzaSalto = vectorSalto.magnitude;
        direccionSalto = vectorSalto.normalized;
        Debug.Log(direccionSalto);

        rb.AddForce(direccionSalto * fuerzaSalto * 2);
      */
        float fuerzaSalto;
        fuerzaSalto = Mathf.Clamp(final.magnitude, 150, 190); ;
        final.x *= -1;
        final.z = 0;
        rb.AddForce(final.normalized * fuerzaSalto*2);

    }

    internal void setPosition(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }
}
