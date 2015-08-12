using UnityEngine;
using System.Collections;

public class EntrenamientoEstandar : ModoPenalties
{
	public override void InicioFase ()
	{
		if (fase < 5) {
			if (rolActual == ModoJuego.Portero) {
				++fase;
				rolActual = ModoJuego.Tirador;
			} else {
				rolActual = ModoJuego.Portero;
				iaPortero.enabled = false;
			}
			ColocarCamara ();
			input.enabled = true;
			reset();
			EmpezarContador ();
		}
	}


}
