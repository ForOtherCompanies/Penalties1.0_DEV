using UnityEngine;
using System.Collections;

public class EntrenamientoParadas : ModoPenalties {
	public override void OnEnable ()
	{	timer = 10;
		rolActual = ModoJuego.Tirador;
		fase = 0;
		ColocarCamara ();
		input.enabled = true;
		EmpezarContador ();
	}
	public override void InicioFase ()
	{
		if (fase>0) {
			rolActual = ModoJuego.Portero;
			ColocarCamara ();
			input.enabled = true;
			reset();
			EmpezarContador ();	
			++fase;
		}
	}
}
