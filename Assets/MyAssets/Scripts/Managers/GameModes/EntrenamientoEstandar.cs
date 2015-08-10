using UnityEngine;
using System.Collections;

public class EntrenamientoEstandar : ModoPenalties {


	//aqui debe ir lo especifico de este modo.. .la orquestacion, el control de cuando se tira, cuando no, la ronda...
	public override void RealizarAcciones (Vector2 inicioTouch, Vector3 destinoTouch)
	{
		Debug.Log ("--> EntrenamientoEstandar");
	}
}
