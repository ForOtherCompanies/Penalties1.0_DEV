using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EntrenamientoEstandar : ModoPenalties
{

	private int contadorPlayer1;
	private int contadorPlayer2;
	public Text textPlayer1;
	public Text textPlayer2;

	public override void  OnEnable (){
		base.OnEnable();
		contadorPlayer1= 0;
		contadorPlayer2 = 0;
		textPlayer1.text = "0";
		textPlayer2.text = "0";

	}
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
			reset ();
			EmpezarContador ();
		} else {
			//volver al menu;
		}
	}


	public override void PuntoConseguido ()
	{
		if (rolActual == ModoJuego.Tirador){
			contadorPlayer1 ++;
			textPlayer1.text = contadorPlayer1.ToString();
		} else{

			contadorPlayer2 ++;
			textPlayer2.text = contadorPlayer2.ToString();
		}
	}
	public  void Update ()
	{
		EsperarJugador ();
		AccionesRealizadas ();
		AccionesIA ();
	}


}
