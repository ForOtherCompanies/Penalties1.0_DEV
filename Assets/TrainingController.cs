using UnityEngine;
using System.Collections;

public class TrainingController : MonoBehaviour {
	public GameModeManager modeManager;
	public CameraManager cameraMngr;

	public void ActivarEntrenador(){
		cameraMngr.DesactivarCamara ();
		modeManager.ActivateEntrenamientoEstandar ();
	}
	public void ActivarTiros(){
		cameraMngr.DesactivarCamara ();
		modeManager.ActivateEntrenamientoDianas ();
	}
	public void ActivarParadas(){
		cameraMngr.DesactivarCamara ();
		modeManager.ActivateEntrenamientoParadas ();
	}

	public void volver(){
		cameraMngr.activarCamara ();
		modeManager.DisableCurrentMode ();
	}
	
}
