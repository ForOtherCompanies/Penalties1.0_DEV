using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	//keep public
		//pointers to each canvas try to convert into an array
		public Canvas goBackCanvas;//delete
		public Canvas mainMenuCanvas;
		public Canvas trainingCanvas;
		public Canvas finRondaCanvas;
		public Canvas multiplayerCanvas;
		public Canvas vestuarioCanvas;
		public Canvas tanteoEstandar;
		//reference to others Manager
		public GameModeManager gameModeManager;
		public CameraManager cameraManager;

	//set private
		private Canvas currentCanvas;
		private Canvas goBackReference = null; //apuntador al menu que hay que activar en caso de pulsar el boton GoBack


	void Start (){
		goBackReference = mainMenuCanvas;
		currentCanvas = mainMenuCanvas;
	}
	
	//vuelve al main o al apuntado por goBackReference
	public void BackToMainMenu(){ 
		gameModeManager.DisableCurrentMode();
		ProtocoloCambioCanvas (goBackReference, currentCanvas);
		DisableAuxiliarCanvases();
		cameraManager.MoverInicio ();
	}

	public void GoToTrainingMenu(){

		ProtocoloCambioCanvas (trainingCanvas, mainMenuCanvas);
		cameraManager.MoverEntrenamiento ();
		
	}	
	public void GoToMultiplayerMenu(){
		
		ProtocoloCambioCanvas (multiplayerCanvas, mainMenuCanvas);
		cameraManager.MoverMultijugador ();

		
	}
	public void GoToVestuarioMenu(){

		ProtocoloCambioCanvas (vestuarioCanvas, mainMenuCanvas);
		cameraManager.MoverVestuario ();
	}


	public void JumpToStandardTrainingMode(){
		
		ProtocoloCambioCanvas (tanteoEstandar, trainingCanvas);
		gameModeManager.ActivateEntrenamientoEstandar ();
	}

	public void JumpToDianaTrainingMode(){

		ProtocoloCambioCanvas (tanteoEstandar, trainingCanvas);
		gameModeManager.ActivateEntrenamientoDianas ();
	}
	public void JumpToGKeeperTrainingMode(){

		ProtocoloCambioCanvas (tanteoEstandar, trainingCanvas);
		gameModeManager.ActivateEntrenamientoParadas ();
	}

	private void DisableAuxiliarCanvases(){
		tanteoEstandar.enabled = false;
	}

	private void ProtocoloCambioCanvas(Canvas  canvasDestino, Canvas canvasOrigen) {
		currentCanvas = canvasDestino;
		goBackReference = canvasOrigen;
	}
}
