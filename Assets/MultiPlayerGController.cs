using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine.UI;

public interface MPUpdateListener
{
	void UpdateReceived (string senderId, Vector3 position, Vector3 velocity);
}

public class MultiPlayerGController : ModoPenalties, MPUpdateListener
{
	
	private string _myParticipantId;
	private ModoJuego rolInicial;
	private int contadorPlayer1;
	private int contadorPlayer2;
	public Text textPlayer1;
	public Text textPlayer2;
	private PhysicManager elemento;

	public override void OnEnable ()
	{
		SetPlayerNumber ();
		rolActual = rolInicial;
		timer = 10;
		fase = 0;
		ColocarCamara ();
		input.enabled = true;
		EmpezarContador ();
		accionRealizada = false;
		pelota.reiniciar ();
		contadorPlayer1 = 0;
		contadorPlayer2 = 0;
		textPlayer1.text = "0";
		textPlayer2.text = "0";
		//acutalizar aspecto contrario
	}

	protected override void InicioFase ()
	{
		if (fase < 5) {
			if (rolActual == ModoJuego.Portero) {
				rolActual = ModoJuego.Tirador;
				elemento = portero;
			} else {
				rolActual = ModoJuego.Portero;
				elemento = pelota;
			}
			if (rolActual == rolInicial) {
				fase++;
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
		if (rolActual == ModoJuego.Tirador) {
			contadorPlayer1 ++;
			textPlayer1.text = contadorPlayer1.ToString ();
		} else {
			
			contadorPlayer2 ++;
			textPlayer2.text = contadorPlayer2.ToString ();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!accionRealizada) {
			if (esperaTiro) {
				contador += Time.deltaTime;
				if (contador > timer) {
					//desconectar
				}
			}
		} else {
			AccionesRealizadas ();
		}
		SendMessagePosition ();
	}

	private void SendMessagePosition(){
		elemento.EnviarMensaje ();
	}

	private void SetPlayerNumber ()
	{
		// 1
		_myParticipantId = MPmanager.Instance.GetMyParticipantId ();
		// 2
		List<Participant> allPlayers = MPmanager.Instance.GetAllPlayers ();
		string nextPlayer = allPlayers [0].ParticipantId;
		if (nextPlayer == _myParticipantId) {
			//comenzar como Tirador;
			rolInicial = ModoJuego.Tirador;
			elemento = portero;
		} else {
			//comenzar como Portero;
			rolInicial = ModoJuego.Portero;
			elemento = pelota;
		}
		
	}

	public override void RealizarAcciones (Vector2 inicioTouch, Vector3 destinoTouch)
	{
	
		if (rolActual == ModoJuego.Tirador) {
			if (PrepararLanzamiento (inicioTouch, destinoTouch)) {
				//pelota.lanzamiento se lanzara desde la animacion del player tirando para que coincida con el momento justo
				////desde aqui lo que habra que hacer es poner la animacion en 'play'
				pelota.Lanzamiento (direccionTiro, fuerzaTiro);
			}
		}
		
		if (rolActual == ModoJuego.Portero) {
			PrepararSaltoPortero (inicioTouch, destinoTouch);
			portero.Saltar (direccionSalto, fuerzaSalto);
		}
		esperaTiro = false;
		accionRealizada = true;
		input.enabled = false;
	}

	public void UpdateReceived (string senderId, Vector3 position, Vector3 velocity)
	{
		List<Participant> allPlayers = MPmanager.Instance.GetAllPlayers ();
		if (allPlayers.Count != 1) {
			if(rolActual == ModoJuego.Portero){
				accionRealizada = true;
			}
			elemento.ActualizarEstado(position,velocity);
		} else {
			//fin de juego;
		}
	}

}
