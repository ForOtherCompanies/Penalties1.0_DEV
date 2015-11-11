using UnityEngine;
using System.Collections;

public class PelotaFisicas : PhysicManager
{

    //references to external
    public GameObject rotationHelper;


    //references to components
    private float maximoEfecto = 8f;

    //set private
    public bool recibiendoEfecto = false;
    private bool tiroIA = false;
    private Vector3 efecto;
    private bool efectoConstanteCalculado = false;
    private float fuerzaEfecto;
    private Vector3 posicionInicio;

    public GameObject tirador;
    public GameObject posicionTirador;


    //debugOnly

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.isKinematic = true;
        efecto = Vector3.zero;
    }
    void FixedUpdate()
    {
        if (recibiendoEfecto)
        {
            if (!tiroIA)
                efecto = CalcularEfectoGyro(rotationHelper.transform.eulerAngles.z);
            else
                if (!efectoConstanteCalculado)
                {
                    efecto = CalcularEfectoConstante();

                }
            rb.AddForce(efecto, ForceMode.Force);
        }
    }

    public void SendInfo(float length, Vector3 fin)
    {
        GameConector.sendBola(length, fin);
    }

    public override void AccionIA(Vector3 direccion, float fuerza, int level)
    {
        rb.isKinematic = false;
        tiroIA = true;
        efectoConstanteCalculado = false;
        rb.AddForce(direccion * fuerza); //esto es la fuerza del tiro NO el efecto
        if (Random.Range(1, 100) < level * 9)
        {
            recibiendoEfecto = true;
            fuerzaEfecto = Random.Range(0, 8);

        }
    }

    //el vector3 retornado incluye direccion+modulo
    Vector3 CalcularEfectoConstante()
    {
        float modulo = Random.Range(-8.0f, 8.0f);
        //		Debug.Log ("efecto constante aplicado"+Vector3.left * modulo);
        efectoConstanteCalculado = true;
        return Vector3.left * modulo;
    }

    //el vector3 retornado incluye direccion+modulo
    Vector3 CalcularEfectoGyro(float angle)
    {
        float factor = 0;

        //si inclina a la izquierda
        if (angle > 0 && angle < 90)
        {
            angle = Mathf.Clamp(angle, 0, 45); //clamp a 45, es decir 45º a derecha es el maximo que se registra para el efecto

        }

        //si inclina a derecha
        if (angle < 360 && angle > 270)
        {
            angle = Mathf.Clamp(angle, 315, 360); //clamp a 45, es decir 45º a derecha es el maximo que se registra para el efecto
            angle -= 360;
        }

        factor = angle * maximoEfecto / 45;
        //si no se inclina factor = 0 por lo que el temEfecto es (0,0,0)
        Vector3 tempEfecto = Vector3.left * factor;

        return tempEfecto;
    }

    public void OnCollisionEnter(Collision col)
    {
        recibiendoEfecto = false;
    }

    public override void reiniciar()
    {
        base.reiniciar();
        recibiendoEfecto = false;
        tirador.transform.position = posicionTirador.transform.position;
        tirador.transform.rotation = posicionTirador.transform.rotation;
    }

    public bool Lanzamiento(float length, Vector3 fin)
    {
        Vector3 direccionTiro = fin;
        float fuerzaTiro;
        direccionTiro = direccionTiro.normalized;

        direccionTiro.z = 1;
        direccionTiro.y *= 0.85f;
        rb.isKinematic = false;
        tiroIA = false;
        fuerzaTiro = Mathf.Clamp(length, 170, 190);
        rb.AddForce(direccionTiro * fuerzaTiro); //esto es la fuerza del tiro NO el efecto
        recibiendoEfecto = true;

        return true;
    }

    internal void setPosition(Vector3 position, Quaternion rotation)
    {

        transform.position = position;
        transform.rotation = rotation;
    }

    internal void SetTirador(GameObject portero)
    {
        tirador = portero;
    }

    internal GameObject GetTirador()
    {
        return tirador;
    }
}
