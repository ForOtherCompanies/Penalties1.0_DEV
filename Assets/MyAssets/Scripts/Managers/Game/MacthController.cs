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
    public GameObject portero;
    public GameObject tirador;
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

    public void RealizarAcciones(float _length, Vector3 _final, Vector3 _Inicio)
    {
        //if 'estamos como delantero y todo esta correcto para lanzar'
        ////then pelota.fisicas.Lanzar (inicio, fin,fuerza);
        length = _length;
        final = _final;
        Debug.Log(rolActual);
        if (rolActual == Rol.Tirador)
        {
            //pelota.lanzamiento se lanzara desde la animacion del player tirando para que coincida con el momento justo
            ////desde aqui lo que habra que hacer es poner la animacion en 'play'
            tirador.GetComponent<AnimationController>().Tirar();
            //activar animacion
            if (Multiplayer)
                pelota.SendInfo(length, final);
            Debug.Log("tirar");

        }

        if (rolActual == Rol.Portero)
        {
            final = final - _Inicio;
            portero.GetComponent<AnimationController>().Saltar();
            //activar animacion
            if (Multiplayer)
                portero.GetComponent<PorteroFisicas>().SendInfo(final);
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

        //activar animacion IA

        tirador.GetComponent<AnimationController>().TirarIA();
    }

    internal void IniciarCicloOutIn()
    {
        // cameraEffects.IniciarCicloOutIn();
    }

    internal void RealizarIAPortero()
    {
        //activar animacion IA
        portero.GetComponent<AnimationController>().SaltarIA();
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

        pelota.SetTirador(portero);
        portero = tirador;
        tirador = pelota.GetTirador();
        this.GetComponent<IAPortero>().SetPortero(portero);
        pelota.reiniciar();
        portero.GetComponent<PorteroFisicas>().reiniciar();
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
    private float length;
    private Vector3 final;
    private Vector3 finalPVP;
    private float lenghtPVP;

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



    public void SetAccion(float lenght, Vector3 final)
    {
        finalPVP = final;
        lenghtPVP = lenght;
        if (lenght < 0)
        {
            portero.GetComponent<AnimationController>().SaltarMP();
        }
        else
        {
            tirador.GetComponent<AnimationController>().TirarMP();
        }
    }

    public void DoAccion(bool MP, bool IA)
    {
        Debug.Log("Accion Realizada");
        if (!MP)
        {
            if (rolActual == Rol.Tirador)
            {
                //pelota.lanzamiento se lanzara desde la animacion del player tirando para que coincida con el momento justo
                ////desde aqui lo que habra que hacer es poner la animacion en 'play'

                if (IA)
                {

                    this.GetComponent<IAPortero>().RealizarAccion();
                }
                else
                {
                    modalidadActivada.RealizarAccion(pelota.Lanzamiento(length, final));
                }

            }

            if (rolActual == Rol.Portero)
            {
                if (!IA)
                    portero.GetComponent<PorteroFisicas>().Saltar(final);
                else
                    this.GetComponent<IATiro>().RealizarAccion();
            }
        }
        else
        {
            if (rolActual == Rol.Tirador)
            {
                //pelota.lanzamiento se lanzara desde la animacion del player tirando para que coincida con el momento justo
                ////desde aqui lo que habra que hacer es poner la animacion en 'play'

                portero.GetComponent<PorteroFisicas>().Saltar(finalPVP);

            }

            if (rolActual == Rol.Portero)
            {
                modalidadActivada.RealizarAccion(pelota.Lanzamiento(lenghtPVP, finalPVP));
            }
        }
    }
}
