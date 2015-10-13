using UnityEngine;
using System.Collections;

public class ModoPvP : ModoJuego
{

    MacthController.Rol rolInicial;

    public override void Inicializar()
    {
        timer = 10;
        if (mController.GetPosition() == 1)
        {
            mController.setRolTirador();
            rolInicial = MacthController.Rol.Tirador;
        }
        else
        {
            mController.setRolPortero();
            rolInicial = MacthController.Rol.Portero;
        }
        fase = 0;
        mController.activarInput();
        EmpezarContador();
        accionRealizada = false;
        mController.reiniciarPelota();
        mController.ColocarCamara();
        mController.enJuego();
    }

    public override void Update()
    {
        EsperarJugador();
        AccionesRealizadas();
    }

    protected override void EsperarJugador()
    {
        if (!accionRealizada)
        {
            if (esperaTiro)
            {
                contador += Time.deltaTime;
                if (contador > timer)
                {
                    GUI.DoBack();
                    esperaTiro = false;
                    accionRealizada = true;
                }
            }
            if (mController.GetRolActual() == MacthController.Rol.Portero)
            {
                contador += Time.deltaTime;
                if (contador > timer)
                {
                    GUI.DoBack();
                }
            }
        }
    }


    protected override void CambioFase()
    {
        if (mController.GetRolActual() == MacthController.Rol.Portero)
        {
            mController.setRolTirador();
        }
        else
        {
            mController.setRolPortero();
        }
        if (mController.GetRolActual() == rolInicial)
        {
            fase++;
        }
        if (fase < 5)
        {

            Debug.Log(fase);
            mController.ColocarCamara();
            mController.activarInput();
            mController.reset();
            EmpezarContador();
            marcado = false;
        }
        else
        {
            GUI.DoBack();

        }
    }
}
