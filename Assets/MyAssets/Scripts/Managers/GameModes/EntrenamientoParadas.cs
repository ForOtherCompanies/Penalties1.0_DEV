using UnityEngine;
using System.Collections;

public class EntrenamientoParadas : ModoPenalties {
	public override void OnEnable ()
	{	timer = 10;
		rolActual = ModoJuego.Portero;
		fase = 0;
		ColocarCamara ();
		input.enabled = true;
		EmpezarContador ();
	}

	protected override void InicioFase ()
	{
		if (fase<10) {
			rolActual = ModoJuego.Portero;
			ColocarCamara ();
			input.enabled = true;
			reset();
			EmpezarContador ();	
			++fase;
		}
	}
	public  void Update ()
	{
		EsperarJugador ();
		AccionesRealizadas ();
		AccionesIA ();
	}
}
