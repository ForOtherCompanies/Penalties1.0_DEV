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
        Debug.Log("Vector mandado Portero" + final);
        GameConector.sendPortero(final);

    }
    public override void AccionIA(Vector3 direccion, float fuerza, int level)
    {
        rb.AddForce(direccion * fuerza * 30f);
        if (direccion.x > 0)
            rb.AddTorque(0, 0, -fuerza);
        else
            rb.AddTorque(0, 0, fuerza);
    }


    internal void Saltar(Vector3 final)
    {
        float fuerzaSalto;
        fuerzaSalto = Mathf.Clamp(final.magnitude, 150, 180); ;
        final.x *= -1;
        final.z = 0;
        rb.AddForce(final.normalized * fuerzaSalto*20f);
        if (final.x > 0)
            rb.AddTorque(0, 0, -fuerzaSalto );
        else
            rb.AddTorque(0, 0, fuerzaSalto);
    }

    internal void setPosition(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }
}
