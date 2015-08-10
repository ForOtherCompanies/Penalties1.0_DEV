using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	private Transform objetivo;
	public float smoothTime = 0.3F;
	private Vector3 velocity = Vector3.zero;
	public float smooth = 2.0F;
	public float tiltAngle = 45.0F;
	private bool mover = false;
	public GameObject [] camaras;
	public Transform [] objetivos;
	private GameObject camara;
	private float X;
	private float Y;
	public GUIManager guiManager;
	private bool rotar= false;
	
	// Update is called once per frame
	void Update () {
		if (mover) {
			Vector3 targetPosition = objetivo.position;
			transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref velocity, smoothTime);
			if(transform.position == objetivo.position){
				mover = false;
				activarCamara();
			}
		}
		if (rotar) {
			float tiltAroundZ = Input.GetAxis ("Horizontal") * tiltAngle;
			Quaternion target = Quaternion.Euler (X, Y, tiltAroundZ);
			transform.rotation = Quaternion.Slerp (transform.rotation, target, Time.deltaTime * smooth);
		}

	}

	public void MoverEntrenamiento(){
		camaras [0].SetActive(false);
		objetivo = objetivos [1];
		camara = camaras [1];
		mover = true;
		rotar = true;
		X = 25;
		Y = 0;
	}
	public void MoverInicio(){
		camaras [1].SetActive(false);
		objetivo = objetivos [0];
		camara = camaras [0];
		mover = true;
		rotar = true;
		X = 40;
		Y = 180;
	}
	void activarCamara(){
		camara.SetActive(true);

	}
}
