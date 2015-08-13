using UnityEngine;
using System.Collections;

public class CameraEffects : MonoBehaviour {

	//keep public
	public Animator fadeCameraAnimator;


	//debug only
	int contador;
	void Update () {

//for debug only
/*
		if(Input.GetKeyDown("space")){
			contador ++;
			if (contador%2 == 1)
				FadeIn();
			else
				FadeOut ();
		}
*/
/////////
	}

	public void IniciarCicloOutIn(){
		fadeCameraAnimator.SetBool ("CicloFadeOutIn", true);
	}

	public void ResetCicloFlag (){
		fadeCameraAnimator.SetBool ("CicloFadeOutIn", false);
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
