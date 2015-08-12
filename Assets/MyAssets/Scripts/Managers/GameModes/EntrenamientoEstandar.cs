using UnityEngine;
using System.Collections;

public class EntrenamientoEstandar : ModoPenalties
{
	private bool esperaTiro;
	private int fase;
	private bool accionIA = false;
	private bool accionRealizada = false;
	private float tiempoParada = 0.5f;
	private float tiempoEntreFases = 3f;
	private float tiempoIATiro = 3.5f;
	private float contadorIA = 0;
	private float contadorCambioFase= 0;
	public GameObject posicionCamaraPortero;
	public GameObject posicionCamaraTirador;



	//keep private

	void OnEnable ()
	{
		timer = 10;

		rolActual = ModoJuego.Tirador;
		fase = 0;
		ColocarCamara ();
		input.enabled = true;
		EmpezarContador ();
	}

	void Start(){
		timer = 10;
		rolActual = ModoJuego.Tirador;
		fase = 0;
		ColocarCamara ();
		input.enabled = true;
		EmpezarContador ();
		if (rolActual == ModoJuego.Portero) {
			iaPortero.enabled = true;
			iaTiro.enabled = false;
		} else {
			iaPortero.enabled = false;
			iaTiro.enabled = true;
		}
	}

	void Update ()
	{

	
		if (esperaTiro) {
			contador += Time.deltaTime;
			if (contador > timer) {
				iaTiro.RealizarAccion();
				accionIA = true;
				esperaTiro = false;
				accionRealizada = true;
			}
		}
		if (accionRealizada) {
			contadorCambioFase+=Time.deltaTime;
			//para sincronizar con el fade in/out
			if (contadorCambioFase > tiempoEntreFases-1)// && cameraEffects.fadeCameraAnimator.GetBool("CicloFadeInOut")==false)
				cameraEffects.IniciarCicloOutIn();

			if(contadorCambioFase>tiempoEntreFases){
				accionRealizada= false;
				InicioFase ();
			}
		}
		if (accionIA) {
			contadorIA+=Time.deltaTime;
			if(rolActual==ModoJuego.Tirador && contadorIA>tiempoParada){
				iaPortero.RealizarAccion();
				accionIA= false;
			}
			if(rolActual == ModoJuego.Portero &&contadorIA>tiempoIATiro){
				accionRealizada = true;
				iaTiro.RealizarAccion();
				accionIA= false;
			}
		}
	}

	//funciona
	public override void RealizarAcciones (Vector2 inicioTouch, Vector3 destinoTouch)
	{
		base.RealizarAcciones (inicioTouch, destinoTouch);//realiza lo que sea con la fisica
		accionIA = true;
		esperaTiro = false;
		accionRealizada = true;

	}

	//funciona
	void EmpezarContador ()
	{
		contador = 0;
		contadorCambioFase = 0;
		contadorIA = 0;
		esperaTiro = true;
	}

	//funciona
	void InicioFase ()
	{
		if (fase < 5) {
			if (rolActual == ModoJuego.Portero) {
				++fase;
				rolActual = ModoJuego.Tirador;
				iaPortero.enabled = true;
				iaTiro.enabled = false;
			} else {
				rolActual = ModoJuego.Portero;
				iaPortero.enabled = false;
				iaTiro.enabled = true;
				accionIA = true;
			}
			ColocarCamara ();
			input.enabled = true;
			reset();
			EmpezarContador ();
		} else {
			finModoJuego ();
		}
	}


	//funciona
	void ColocarCamara(){

		//cameraEffects.IniciarCicloOutIn();

		if (rolActual == ModoJuego.Portero) {

			mainCamera.transform.position = posicionCamaraPortero.transform.position;
			//cameraEffects.FadeOut();
			//cameraEffects.FadeIn();
			//mainCamera.transform.Rotate(0f,180f, 0f);

			mainCamera.transform.rotation = posicionCamaraPortero.transform.rotation;
		} else {

			mainCamera.transform.position = posicionCamaraTirador.transform.position;
			//cameraEffects.FadeOut();
			//cameraEffects.FadeIn();
		//	mainCamera.transform.Rotate(0f,0f, 0f);
			
			mainCamera.transform.rotation = posicionCamaraTirador.transform.rotation;
		}
	}


	public void reset(){
		pelota.reiniciar();
		portero.reiniciar ();
	}


	public override void finModoJuego ()
	{

	}
}
