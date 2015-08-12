using UnityEngine;
using System.Collections;


//esta es para funcionar como VIRTUAL tambien
public class ModoPenalties : GameModeVirtual {
	//aqui van variables para saber el estado del juego (p.ej. si estamos en modo portero o tirador... y para
	//tambien es responasbildad de este manager alternar entre ellos. De momento solo tenemos el tiro a puerta
	public enum ModoJuego{Tirador, Portero};
	
	//keepPublic
	public GameObject mainCamera;
	public InputManager input;
	public IAPortero iaPortero;
	public IATiro iaTiro;
	public PelotaFisicas pelota;
	public PorteroFisicas portero;
	public GameObject pelotaPosicionLanzamiento;
	public GameObject posicionPortero;
	public GameObject posicionCamaraPortero;
	public GameObject posicionCamaraTirador;
	
	//keep protected
	protected ModoJuego rolActual;
	protected float timer;
	protected float contador;
	protected int fase;
	protected bool accionIA = false;
	//setPrivate
	//// variables para el lanzamiento
	private Vector3 direccionTiro;
	private float fuerzaTiro;
	//// variables para el portero
	private Vector3 direccionSalto;
	private float fuerzaSalto;
	
	private bool esperaTiro;
	private bool accionRealizada = false;
	private float tiempoParada = 0.5f;
	private float tiempoEntreFases = 3f;
	private float tiempoIATiro = 3.5f;
	private float contadorIA = 0;
	private float contadorCambioFase= 0;
	
	

	public virtual void OnEnable ()
	{
		timer = 10;
		rolActual = ModoJuego.Tirador;
		fase = 0;
		ColocarCamara ();
		input.enabled = true;
		EmpezarContador ();
	}
	//debera desaparecer luego sera solo el OnEnable
	public virtual void Start(){
		timer = 10;
		rolActual = ModoJuego.Tirador;
		fase = 0;
		ColocarCamara ();
		input.enabled = true;
		EmpezarContador ();
	}
	
	public virtual void Update ()
	{
		if (esperaTiro) {
			contador += Time.deltaTime;
			if (contador > timer) {
				iaTiro.RealizarAccion();
				//accionIA = true;
				esperaTiro = false;
				accionRealizada = true;
			}
		}
		if (accionRealizada) {
			contadorCambioFase+=Time.deltaTime;
			//para sincronizar con el fade in/out
			if (contadorCambioFase > tiempoEntreFases-1)
				cameraEffects.IniciarCicloOutIn();
			
			if(contadorCambioFase>tiempoEntreFases){
				accionRealizada= false;
				InicioFase ();
			}
		}
		if (accionIA) {
			contadorIA+=Time.deltaTime;
			if(rolActual==ModoJuego.Tirador && contadorIA>tiempoParada){
				if(iaPortero!=null)
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
	public void EmpezarContador ()
	{
		contador = 0;
		contadorCambioFase = 0;
		contadorIA = 0;
		esperaTiro = true;
	}
	
	//funciona overrride TODO
	public virtual void InicioFase ()
	{}
	
	
	//funciona
	public void ColocarCamara(){
		
		//cameraEffects.IniciarCicloOutIn();
		
		if (rolActual == ModoJuego.Portero) {
			
			mainCamera.transform.position = posicionCamaraPortero.transform.position;
			mainCamera.transform.rotation = posicionCamaraPortero.transform.rotation;
		} else {
			
			mainCamera.transform.position = posicionCamaraTirador.transform.position;	
			mainCamera.transform.rotation = posicionCamaraTirador.transform.rotation;
		}
	}
	
	
	public void reset(){
		pelota.reiniciar();
		if(portero!=null)
			portero.reiniciar ();
	}

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
		esperaTiro = false;
		accionRealizada = true;
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
}
