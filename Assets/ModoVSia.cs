using UnityEngine;
using System.Collections;

public class ModoVSia : ModoJuego
{



    public override void Update()
    {
        EsperarJugador();
        AccionesRealizadas();
        AccionesIA();
    }

    protected override void CambioFase()
    {
        if (mController.GetRolActual() == MacthController.Rol.Portero)
        {
            ++fase;
            mController.setRolTirador();
        }
        else
        {
            mController.setRolPortero();
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
