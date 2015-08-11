using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
	public CameraManager cameraManager;

	public void moverEntrenamiento(){
		cameraManager.MoverEntrenamiento ();

	}
	public void MoverInicio(){
		cameraManager.MoverInicio ();
	}
	public void moverMultijugador(){
		cameraManager.MoverMultijugador ();
	}
}
