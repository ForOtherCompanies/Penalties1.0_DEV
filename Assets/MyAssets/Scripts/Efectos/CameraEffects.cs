using UnityEngine;
using System.Collections;

public class CameraEffects : MonoBehaviour {

	//keep public
	public Animator fadeCameraAnimator;
	public Canvas fadeEffectCanvas;

	//debug only
	int contador;

	void Start (){
		fadeEffectCanvas.enabled = false;
	}



	public void IniciarCicloOutIn(){
		fadeEffectCanvas.enabled = true;
		fadeCameraAnimator.SetBool ("CicloFadeOutIn", true);
	}

	public void ResetCicloFlag (){
		fadeCameraAnimator.SetBool ("CicloFadeOutIn", false);
		fadeEffectCanvas.enabled = false;
	}
/*
	public void FadeIn () {

		fadeCameraAnimator.SetBool ("FadeIn", true);
		fadeCameraAnimator.SetBool ("FadeOut", false);
		Debug.Log ("FADE IN hecho");

	
	}

	public void FadeOut () {

		fadeCameraAnimator.SetBool ("FadeIn", false);
		fadeCameraAnimator.SetBool ("FadeOut", true);
		Debug.Log ("FADE OUT hecho");

	}
*/


}
