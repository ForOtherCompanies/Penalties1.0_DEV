using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	//aqui van variables para saber el estado del juego (p.ej. si estamos en modo portero o tirador... y para
	//tambien es responasbildad de este manager alternar entre ellos. De momento solo tenemos el tiro a puerta


	//keepPublic
	public PelotaFisicas pelota;
	public GameObject pelotaPosicionLanzamiento;
	public Camera mainCamera;


	//setPrivate
	//// variable para el lanzamiento
	public Vector3 direccionTiro;
	public float fuerzaTiro;



	//only for debug
	public GameObject raycastMark;



	public void RealizarAcciones(Vector2 inicioTouch, Vector3 destinoTouch){
		//if 'estamos como delantero y todo esta correcto para lanzar'
		////then pelota.fisicas.Lanzar (inicio, fin,fuerza);
		if (PrepararLanzamiento (inicioTouch, destinoTouch))
			pelota.Lanzaminento(direccionTiro, fuerzaTiro);


	}

	//nos tiene que devolver el vector direccion y la fuerza del lanzamiento.
	bool PrepararLanzamiento (Vector2 inicio, Vector2 fin){

		RaycastHit hit;
		Ray ray = mainCamera.ScreenPointToRay(fin);

		if (Physics.Raycast (ray, out hit, 500)){
			if (hit.transform.tag == "RaycastReactor"){

				direccionTiro =  hit.point - pelotaPosicionLanzamiento.transform.position;
				direccionTiro = direccionTiro.normalized;

				fuerzaTiro =  Vector2.Distance(fin,inicio);

				
		 		Debug.DrawLine (pelotaPosicionLanzamiento.transform.position, hit.point, Color.red, 5000);
			 	Debug.DrawLine (ray.origin, ray.direction * 500, Color.yellow, 5000);
				Debug.Log (fuerzaTiro);

				return true;
			}

			return false;

		}

		return false;

	}


/*

*/
}
