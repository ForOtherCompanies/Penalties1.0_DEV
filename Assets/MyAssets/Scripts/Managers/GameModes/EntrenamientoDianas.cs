using UnityEngine;
using System.Collections;

public class EntrenamientoDianas : ModoPenalties {
	private GameObject diana;

	public override void InicioFase ()
	{
		if (fase<10) {
			rolActual = ModoJuego.Portero;
			ColocarCamara ();
			input.enabled = true;
			reset();
			EmpezarContador ();
			if(diana == null){
				CrearDiana();
				fase++;
			}
			
		}
	}
	
	void CrearDiana()
	{
		//crear una diana en un punto random de la porteria
	}

}
