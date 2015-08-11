using UnityEngine;
using System.Collections;

public class EntrenamientoEstandar : ModoPenalties
{
	private bool esperaTiro;
	private int fase;
	private bool accionIA = false;
	private bool accionRealizada = false;
	private float tiempoParada = 0.75f;
	private float tiempoEntreFases = 3f;
	private float tiempoIATiro = 3.5f;
	private float contadorIA = 0;
	private float contadorCambioFase= 0;
	public GameObject posicionCamaraPortero;
	public GameObject posicionCamaraTirador;

	void OnEnable ()
	{
		timer = 10;
		rolActual = ModoJuego.Tirador;
		fase = 0;
		InicioFase ();
	}

	void Update ()
	{
		if (esperaTiro) {
			contador += Time.deltaTime;
			if (contador > timer) {
				iaTiro.RealizarAccion();
				accionIA = true;
				esperaTiro = false;
				InicioFase();
			}
		}
		if (accionRealizada) {
			contadorCambioFase+=Time.deltaTime;
			if(contador>tiempoEntreFases){
				accionRealizada= false;
				InicioFase ();
			}
		}
		if (accionIA) {
			contadorIA+=Time.deltaTime;
			if(rolActual==ModoJuego.Portero && contadorIA>tiempoParada){
				iaPortero.RealizarAccion();
				accionIA= false;
			}
			if(rolActual == ModoJuego.Tirador &&contadorIA>tiempoIATiro){
				iaTiro.RealizarAccion();
				accionIA= false;
			}
		}
	}

	public override void RealizarAcciones (Vector2 inicioTouch, Vector3 destinoTouch)
	{
		base.RealizarAcciones (inicioTouch, destinoTouch);//realiza lo que sea con la fisica
		accionIA = true;
		esperaTiro = false;
		accionRealizada = true;

	}

	void EmpezarContador ()
	{
		this.contador = 0;
		esperaTiro = true;
	}

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
			}
			ColocarCamara ();
			input.enabled = true;
			EmpezarContador ();
		} else {
			finModoJuego ();
		}
	}

	void ColocarCamara(){
		float x,y,z;
		x = mainCamera.transform.rotation.x;
		y = mainCamera.transform.rotation.y;
		z = mainCamera.transform.rotation.z;
		if (rolActual == ModoJuego.Portero) {
			mainCamera.transform.position = posicionCamaraPortero.transform.position;
			mainCamera.transform.Rotate(25f-x,180f-y, 0f-z, Space.World);
		} else {
			mainCamera.transform.position = posicionCamaraTirador.transform.position;
			
			mainCamera.transform.Rotate(25f-x,0f-y, 0f-z, Space.World);
		}
	}

	public override void finModoJuego ()
	{

	}
}
