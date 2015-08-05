using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	//aqui van variables para saber el estado del juego (p.ej. si estamos en modo portero o tirador... y para
	//tambien es responasbildad de este manager alternar entre ellos. De momento solo tenemos el tiro a puerta


	//keepPublic
	public GameObject pelota;

	//setPrivate
	//// variable para el lanzamiento
	public Vector3 direccionTiro;
	public Vector3 fuerzaTiro;



	//only for debug
	public GameObject raycastMark;



	public void RealizarAcciones(Vector2 inicioTouch, Vector3 destinoTouch){
		//if 'estamos como delantero y todo esta correcto para lanzar'
		////then pelota.fisicas.Lanzar (inicio, fin,fuerza);
		PrepararLanzamiento (inicioTouch, destinoTouch);


	}


	void PrepararLanzamiento (Vector2 inicio, Vector2 fin){
		//raycast y calculo de direccionTiro y fuerzaTiro
	}



}
