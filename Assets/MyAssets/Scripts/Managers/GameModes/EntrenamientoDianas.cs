using UnityEngine;
using System.Collections;

public class EntrenamientoDianas : ModoPenalties
{
	public GameObject diana;
	public GameObject PjPortero;
	private GameObject dianaClon;
	private static float MinX = -7.2f;
	private static float MaxX = 0.56f;
	private static float MinY = 0.8f;
	private static float MaxY = 1.6f;
	private static float Z = 11.0f;

	public override void OnEnable ()
	{
		base.OnEnable ();
		PjPortero.gameObject.SetActive (false);
		CrearDiana ();
	}

	/*
	 * pensar en poner un tiempo como 30" y ver cuantas dianas rompe en ese tiempo
	 */
	protected override void InicioFase ()
	{
		if (fase < 10) {
			rolActual = ModoJuego.Tirador;
			ColocarCamara ();
			input.enabled = true;
			reset ();
			EmpezarContador ();
			if (dianaClon == null) {
				CrearDiana ();
				fase++;
			}
		}
	}

	public void Update ()
	{
		EsperarJugador ();
		AccionesRealizadas ();
	}


	void CrearDiana ()
	{
		float x, y;
		x = Random.Range (MinX, MaxX);
		y = Random.Range (MinY, MaxY);
		Vector3 posicion = new Vector3 (x, y, Z);
		dianaClon = (GameObject)Instantiate (diana, posicion, transform.rotation);
	}

}
