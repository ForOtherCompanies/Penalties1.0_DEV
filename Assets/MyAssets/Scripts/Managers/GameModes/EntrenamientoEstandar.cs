using UnityEngine;
using System.Collections;

public class EntrenamientoEstandar : ModoPenalties
{
	protected override void InicioFase ()
	{
		if (fase < 5) {
			if (rolActual == ModoJuego.Portero) {
				++fase;
				rolActual = ModoJuego.Tirador;
			} else {
				rolActual = ModoJuego.Portero;
			}
			ColocarCamara ();
			input.enabled = true;
			reset();
			EmpezarContador ();
		}
	}
	public  void Update ()
	{
		EsperarJugador ();
		AccionesRealizadas ();
		AccionesIA ();
	}


}
