using UnityEngine;
using System.Collections;

public class MacthController : MonoBehaviour
{

    ModoJuego modalidadActivada = null;
    public enum Rol
    {
        Tirador,
        Portero
    }
	;
    Rol rolActual;
    public PelotaFisicas pelota;
    public Camera gameCamera;
    public PorteroFisicas portero;
    public GameObject posicionCamaraPortero;
    public GameObject posicionCamaraTirador;
    public gameUI GUI;
    // public CameraEffects cameraEffects;

    private int posicion;
    private bool Multiplayer;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (modalidadActivada != null)
            modalidadActivada.Update();
    }


    public void ActivarModoActual(ModoJuego modo)
    {
        modalidadActivada = modo;
        modalidadActivada.SetMController(this);
        modalidadActivada.Inicializar();
        modalidadActivada.SetGUI(GUI);
        this.GetComponent<InputManager>().enabled = true;
        reset();
    }

    public void enJuego()
    {
        Multiplayer = true;
    }

    public void RealizarAcciones(float length, Vector3 final)
    {
        //if 'estamos como delantero y todo esta correcto para lanzar'
        ////then pelota.fisicas.Lanzar (inicio, fin,fuerza);
        if (rolActual == Rol.Tirador)
        {
            //pelota.lanzamiento se lanzara desde la animacion del player tirando para que coincida con el momento justo
            ////desde aqui lo que habra que hacer es poner la animacion en 'play'
            modalidadActivada.RealizarAccion(pelota.Lanzamiento(length, final));
            pelota.SendInfo(length, final);
            Debug.Log("tirar");

        }

        if (rolActual == Rol.Portero)
        {
            portero.Saltar(final);
            portero.SendInfo(final);
            Debug.Log("Saltar");
        }
        this.GetComponent<InputManager>().enabled = false;
    }

    internal void setRolTirador()
    {
        rolActual = Rol.Tirador;
    }


    public bool AccionRealizada { get; set; }

    internal void activarInput()
    {
        this.GetComponent<InputManager>().enabled = true;
    }

    internal void reiniciarPelota()
    {
        pelota.reiniciar();
    }

    internal Rol GetRolActual()
    {
        return rolActual;
    }

    internal void RealizarIATiro()
    {
        this.GetComponent<IATiro>().RealizarAccion();
    }

    internal void IniciarCicloOutIn()
    {
        // cameraEffects.IniciarCicloOutIn();
    }

    internal void RealizarIAPortero()
    {
        this.GetComponent<IAPortero>().RealizarAccion();
    }

    public void ColocarCamara()
    {

        //cameraEffects.IniciarCicloOutIn();

        if (rolActual == Rol.Portero)
        {

            gameCamera.transform.position = posicionCamaraPortero.transform.position;
            gameCamera.transform.rotation = posicionCamaraPortero.transform.rotation;
        }
        else
        {
            gameCamera.transform.position = posicionCamaraTirador.transform.position;
            gameCamera.transform.rotation = posicionCamaraTirador.transform.rotation;
        }
    }

    public void reset()
    {
        pelota.reiniciar();
        portero.reiniciar();
    }


    internal void setRolPortero()
    {
        rolActual = Rol.Portero;
    }

    public void Desactivar()
    {
        modalidadActivada = null;
        this.GetComponent<InputManager>().enabled = false;
    }

    internal void PuntoConseguido()
    {
        modalidadActivada.Marcado(true);
    }

    internal void AccionGol(bool marcado, int fase, bool p)
    {
        GUI.AccionGol(marcado, fase, p);
    }

    string nameOP = "Com";

    public void SetOponentName(string nombre)
    {
        nameOP = nombre;
    }
    public string GetOponentName()
    {
        return nameOP;
    }




    internal void SetPosition(int p)
    {
        posicion = p;
    }

    internal int GetPosition()
    {
        return posicion;
    }

    internal void SetBallPosition(Vector3 position, Quaternion rotation)
    {
        pelota.setPosition(position, rotation);
    }

    internal void SetPorteroPosition(Vector3 position, Quaternion rotation)
    {
        portero.setPosition(position, rotation);
    }



    public void SetAccion(float lenght, Vector3 final)
    {
        if (lenght < 0)
        {
            portero.Saltar(final);
        }
        else
        {
            pelota.Lanzamiento(lenght, final);
        }
    }
}
