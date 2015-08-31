using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	//keep public
	public GUIManager guiManager;
	public Camera [] camaras;
	public Transform [] objetivos;
	//keep private
	private float smoothTime = 0.3F;
	private Transform objetivo;
	private Vector3 velocity = Vector3.zero;
	private float smooth = 2.0F;
	private bool mover = false;
	private Camera camara;
	private bool rotar= false;
	private Quaternion target;
	private Vector3 targetPosition;

	void Start(){
		camara = camaras [0];
	}
	
	// Update is called once per frame
	void Update () {
		if (mover) {

			transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref velocity, smoothTime);
			if(transform.position == objetivo.position){
				mover = false;
				activarCamara();
			}
		}
		if (rotar) {
			transform.rotation = Quaternion.Slerp (transform.rotation, target, Time.deltaTime * smooth);
			if(transform.rotation== target){
				rotar = false;
			}
		}

	}

	public void MoverEntrenamiento(){
		camara.enabled = false;
		objetivo = objetivos [1];
		camara = camaras [1];
		mover = true;
		rotar = true;
		target =  Quaternion.Euler (25, 0, 0);
		targetPosition = objetivo.position;
	}

	public void MoverMultijugador(){
		camara.enabled = false;
		objetivo = objetivos [2];
		camara = camaras [2];
		mover = true;
		rotar = true;
		target =  Quaternion.Euler (30, 270, 0);
		targetPosition = objetivo.position;
	}
	public void MoverInicio(){
		camara.enabled = false;
		objetivo = objetivos [0];
		camara = camaras [0];
		mover = true;
		rotar = true;
		target= Quaternion.Euler (40, 180, 0);
		targetPosition = objetivo.position;
	}
	void activarCamara(){
		camara.enabled = true;
	}
}
