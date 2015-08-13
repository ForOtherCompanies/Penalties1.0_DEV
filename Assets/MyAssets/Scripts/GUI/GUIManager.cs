using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
	public CameraManager cameraManager;

	//keep public
	//pointers to each canvas
	public Canvas goBackCanvas;
	public Canvas mainMenuCanvas;
	public Canvas trainingCanvas;
	//reference to gameModeManager
	public GameModeManager gameModeManager;

	//set private
	public Canvas currentCanvas;

	void Start (){
		mainMenuCanvas.enabled = true;
		trainingCanvas.enabled = false;
	}

	
	public void moverEntrenamiento(){
		cameraManager.MoverEntrenamiento ();

	}

	public void BackToMainMenu(){
		gameModeManager.DisableCurrentMode();
		currentCanvas.enabled = false;
		currentCanvas = mainMenuCanvas;

		currentCanvas.enabled = true;
	}

	public void GoToTrainingMenu(){
		currentCanvas.enabled = false;
		currentCanvas = trainingCanvas;
		currentCanvas.enabled = true;

	}

	public void JumpToStandardTrainingMode(){
		gameModeManager.ActivateEntrenamientoEstandar ();
		currentCanvas.enabled = false;

		//y activar tambien un 'GoBackCanvas' para volver al menu principal que sera comun a todos
		//los modos. El tipico boton de X para salir

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
