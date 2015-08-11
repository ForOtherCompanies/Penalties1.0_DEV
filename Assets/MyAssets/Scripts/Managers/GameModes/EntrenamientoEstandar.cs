using UnityEngine;
using System.Collections;

public class EntrenamientoEstandar : ModoPenalties
{
	private bool esperaTiro;
	private int fase;
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
			if (contador == timer) {
				//RealizarAcciones (inicioTouch, destinoTouch);//calcular los puntos para que vaya al centro
			}
		}
	}

	public override void RealizarAcciones (Vector2 inicioTouch, Vector3 destinoTouch)
	{
		base.RealizarAcciones (inicioTouch, destinoTouch);//realiza lo que sea con la fisica
		if (rolActual == ModoJuego.Tirador) {
			//iaPortero.parar();
		}
		InicioFase ();//cambia a una nueva fase

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
