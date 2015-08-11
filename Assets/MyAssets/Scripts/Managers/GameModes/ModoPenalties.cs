﻿using UnityEngine;
using System.Collections;


//esta es para funcionar como VIRTUAL tambien
public class ModoPenalties : GameModeVirtual {
	//aqui van variables para saber el estado del juego (p.ej. si estamos en modo portero o tirador... y para
	//tambien es responasbildad de este manager alternar entre ellos. De momento solo tenemos el tiro a puerta
	public enum ModoJuego{Tirador, Portero};
	
	//keepPublic
	public PelotaFisicas pelota;
	public PorteroFisicas portero;
	public GameObject pelotaPosicionLanzamiento;
	public GameObject posicionPortero;
	public GameObject mainCamera;
	public InputManager input;
	public IAPortero iaPortero;
	public IATiro iaTiro;
	
	//keep protected
	protected ModoJuego rolActual;
	protected float timer;
	protected float contador;
	//setPrivate
	//// variables para el lanzamiento
	private Vector3 direccionTiro;
	private float fuerzaTiro;
	//// variables para el portero
	private Vector3 direccionSalto;
	private float fuerzaSalto;


	public override void RealizarAcciones (Vector2 inicioTouch, Vector3 destinoTouch)
	{
		//if 'estamos como delantero y todo esta correcto para lanzar'
		////then pelota.fisicas.Lanzar (inicio, fin,fuerza);
		if (rolActual == ModoJuego.Tirador){
			if (PrepararLanzamiento (inicioTouch, destinoTouch)) {
				//pelota.lanzamiento se lanzara desde la animacion del player tirando para que coincida con el momento justo
				////desde aqui lo que habra que hacer es poner la animacion en 'play'
				pelota.Lanzamiento (direccionTiro, fuerzaTiro);
				return;
			}
		}
		
		if (rolActual == ModoJuego.Portero){
			PrepararSaltoPortero (inicioTouch, destinoTouch);
			portero.Saltar (direccionSalto, fuerzaSalto);
		}
	}

	//nos tiene que devolver el vector direccion y la fuerza del lanzamiento.
	//bool PrepararLanzamiento (Vector2 inicio, Vector2 fin, bool parar)
	bool PrepararLanzamiento (Vector2 inicio, Vector2 fin)
	{
		
		RaycastHit hit;
		Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay (fin);
		
		if (Physics.Raycast (ray, out hit, 500)) {
			if (hit.transform.tag == "RaycastReactor") {
				direccionTiro = hit.point - pelotaPosicionLanzamiento.transform.position;
				direccionTiro = direccionTiro.normalized;
				direccionTiro.y *= 2.68f;
				fuerzaTiro = Vector2.Distance (fin, inicio);
				
				//Debug.DrawLine (ray.origin, ray.direction * 500, Color.yellow, 5000);
				//Debug.Log (fuerzaTiro);
				return true;
			}
			return false;
		}
		
		return false;
		
	}
	
	void PrepararSaltoPortero(Vector2 inicio, Vector2 fin){
		Vector3 vectorSalto = fin-inicio;
		
		//si la potencia del salto es demasiado grande se clampea a 150
		if (vectorSalto.magnitude > 150)
			vectorSalto = Vector3.ClampMagnitude (vectorSalto, 150);
		
		//partimos el vector en direccion+magnitud para mandarselo al script de fisicas del portero
		fuerzaSalto = vectorSalto.magnitude;
		direccionSalto = vectorSalto.normalized;
		
	}
	virtual public void finModoJuego(){
	}
}
