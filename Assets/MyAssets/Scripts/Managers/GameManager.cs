using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

	//aqui van variables para saber el estado del juego (p.ej. si estamos en modo portero o tirador... y para
	//tambien es responasbildad de este manager alternar entre ellos. De momento solo tenemos el tiro a puerta


	//keepPublic
	public PelotaFisicas pelota;
	public PorteroFisicas portero;
	public GameObject pelotaPosicionLanzamiento;
	public GameObject posicionPortero;
	public Camera mainCamera;


	//setPrivate
	//// variable para el lanzamiento
	public Vector3 direccionTiro;
	public float fuerzaTiro;



	//only for debug
	public GameObject raycastMark;

	public void RealizarAcciones (Vector2 inicioTouch, Vector3 destinoTouch, bool parar)
	{
		//if 'estamos como delantero y todo esta correcto para lanzar'
		////then pelota.fisicas.Lanzar (inicio, fin,fuerza);
		if (PrepararLanzamiento (inicioTouch, destinoTouch, parar)) {
			if (!parar)
				pelota.Lanzaminento (direccionTiro, fuerzaTiro);
			else
				portero.Saltar (direccionTiro, fuerzaTiro);
		}

	}

	//nos tiene que devolver el vector direccion y la fuerza del lanzamiento.
	bool PrepararLanzamiento (Vector2 inicio, Vector2 fin, bool parar)
	{

		RaycastHit hit;
		Ray ray = mainCamera.ScreenPointToRay (fin);

		if (Physics.Raycast (ray, out hit, 500)) {
			if (hit.transform.tag == "RaycastReactor") {

				if (parar) {
					direccionTiro = hit.point - posicionPortero.transform.position;
					direccionTiro.z = 0;
				}else
					direccionTiro = hit.point - pelotaPosicionLanzamiento.transform.position;
				direccionTiro = direccionTiro.normalized;
				direccionTiro.y *= 2.68f;
				fuerzaTiro = Vector2.Distance (fin, inicio);
				if(parar){
					if(direccionTiro.y<0.5f)
						direccionTiro.y=0.5f;
					if(direccionTiro.y>0.7f)
						direccionTiro.y=0.7f;
					if(fuerzaTiro>165f)
						fuerzaTiro=165f;
				}

				if(parar)
					Debug.DrawLine (posicionPortero.transform.position, hit.point, Color.red, 5000);
				else
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
