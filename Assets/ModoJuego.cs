using UnityEngine;
using System.Collections;

public class ModoJuego
{
    // Update is called once per frame

    protected gameUI GUI;
    protected MacthController mController;
    protected float timer;
    protected int fase=0;
    protected float contador;
    protected float contadorCambioFase= 0;
    protected float contadorIA = 0;
    protected bool esperaTiro;
    protected bool accionIA;
    protected bool accionRealizada;
    private float tiempoEntreFases= 4f;
    private float tiempoParada = 0.5f;
    private float tiempoIATiro= 3f;
    private int contadorPlayer1=0;
    private int contadorPlayer2=0;
    protected bool marcado=false;

    public virtual void Update()
    {

    }

    public virtual void Inicializar()
    {

        timer = 10;
        mController.setRolTirador();
        fase = 0;
        mController.activarInput();
        EmpezarContador();
        accionRealizada = false;
        mController.reiniciarPelota();
        mController.ColocarCamara();
    }

    public ModoJuego()
    {

    }



    public virtual void SetMController(MacthController _mController)
    {
        mController = _mController;
    }

    protected void EmpezarContador()
    {
        contador = 0;
        contadorCambioFase = 0;
        contadorIA = 0;
        if (mController.GetRolActual() == MacthController.Rol.Tirador)
        {
            esperaTiro = true;
        }
        else
        {
            accionIA = true;
            esperaTiro = false;
        }
    }



    public virtual void RealizarAccion(bool p)
    {
        if (p)
        {
            esperaTiro = false;
            accionRealizada = true;
        }
        if (mController.GetRolActual() == MacthController.Rol.Tirador)
        {
            accionIA = true;
        }

    }


    protected virtual void EsperarJugador()
    {
        if (!accionRealizada)
        {
            if (esperaTiro)
            {
                contador += Time.deltaTime;
                if (contador > timer)
                {
                    mController.RealizarIATiro();
                    accionIA = true;
                    esperaTiro = false;
                    accionRealizada = true;
                }
            }
            if (mController.GetRolActual() == MacthController.Rol.Portero)
            {
                if (!accionIA)
                {
                    contador += Time.deltaTime;
                    if (contador > 1.5)
                    {
                        accionRealizada = true;
                    }
                }
            }
        }
    }

    protected virtual void AccionesRealizadas()
    {
        if (accionRealizada)
        {
            contadorCambioFase += Time.deltaTime;
            //para sincronizar con el fade in/out
            if (contadorCambioFase > tiempoEntreFases - 2)
                PuntoConseguido();
            if (contadorCambioFase > tiempoEntreFases - 1)
                mController.IniciarCicloOutIn();

            if (contadorCambioFase > tiempoEntreFases)
            {
                accionRealizada = false;
                CambioFase();
            }
        }
    }

    protected virtual void CambioFase()
    {
    }

    protected virtual void AccionesIA()
    {
        if (accionIA)
        {
            //Debug.Log (contadorIA);
            contadorIA += Time.deltaTime;
            if (mController.GetRolActual() == MacthController.Rol.Tirador && contadorIA > tiempoParada)
            {
                mController.RealizarIAPortero();
                accionIA = false;
            }
            if (mController.GetRolActual() == MacthController.Rol.Portero && contadorIA > tiempoIATiro)
            {
                mController.RealizarIATiro();
                accionIA = false;
            }
        }
    }

    public void SetGUI(gameUI _GUI)
    {
        GUI = _GUI;
    }


    public void PuntoConseguido()
    {
        if (mController.GetRolActual() == MacthController.Rol.Tirador)
        {
            if (marcado)
            {
                contadorPlayer1++;
            }
           mController.AccionGol(marcado, fase, true);
        }
        else
        {
            mController.AccionGol(marcado, fase, false);
            if (marcado)
            {
                contadorPlayer2++;
            }
        }
    }

    internal void Marcado(bool p)
    {
        marcado = p;
    }
}