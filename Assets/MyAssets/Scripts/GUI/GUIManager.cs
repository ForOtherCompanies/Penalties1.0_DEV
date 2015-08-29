using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
	public CameraManager cameraManager;

	//keep public
	//pointers to each canvas
	public Canvas goBackCanvas;
	public Canvas mainMenuCanvas;
	public Canvas trainingCanvas;
	public Canvas finRondaCanvas;
	public Canvas tanteoEstandar;
	//reference to gameModeManager
	public GameModeManager gameModeManager;

	//set private
	public Canvas currentCanvas;
	public Canvas goBackReference = null; //apuntador al menu que hay que activar en caso de pulsar el boton GoBack
	void Start (){
		mainMenuCanvas.enabled = true;
		trainingCanvas.enabled = false;
		finRondaCanvas.enabled = false;
	}

	
	public void moverEntrenamiento(){
		cameraManager.MoverEntrenamiento ();

	}

	//vuelve al main o al apuntado por goBackReference
	public void BackToMainMenu(){ 
		gameModeManager.DisableCurrentMode();
		currentCanvas.enabled = false;
		if (goBackReference != null)
			currentCanvas = goBackReference;
		else
			currentCanvas = mainMenuCanvas;

		currentCanvas.enabled = true;
		goBackReference = null;
		gameModeManager.currentGameMode = null;

		DisableAuxiliarCanvases();
	}

	public void GoToTrainingMenu(){
		currentCanvas.enabled = false;
		currentCanvas = trainingCanvas;
		currentCanvas.enabled = true;

		goBackReference = mainMenuCanvas;

	}

	public void JumpToStandardTrainingMode(){
		gameModeManager.ActivateEntrenamientoEstandar ();

		currentCanvas.enabled = false;
		tanteoEstandar.enabled = true;
		//y activar tambien un 'GoBackCanvas' para volver al menu principal que sera comun a todos
		//los modos. El tipico boton de X para salir

	}

	private void DisableAuxiliarCanvases(){
		tanteoEstandar.enabled = false;
	}
/*
	public void MoverInicio(){
		cameraManager.MoverInicio ();
	}
	public void moverMultijugador(){
		cameraManager.MoverMultijugador ();
	}
*/
}
