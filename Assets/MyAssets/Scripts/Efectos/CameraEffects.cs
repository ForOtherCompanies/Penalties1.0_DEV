using UnityEngine;
using System.Collections;

public class CameraEffects : MonoBehaviour {

	//keep public
	public Animator fadeCameraAnimator;
/*
	void Update () {

		if(Input.GetKeyDown("space")){
			if (fadeCameraAnimator.GetBool ("FadeIn"))
				fadeCameraAnimator.SetBool ("FadeIn", false);
			else
				fadeCameraAnimator.SetBool ("FadeIn", true);


			if (fadeCameraAnimator.GetBool ("FadeOut"))
				fadeCameraAnimator.SetBool ("FadeOut", false);
			else
				fadeCameraAnimator.SetBool ("FadeOut", true);
		}

	}
*/
	public void FadeIn () {
		fadeCameraAnimator.SetBool ("FadeIn", true);
		fadeCameraAnimator.SetBool ("FadeOut", false);
	}

	public void FadeOut () {
		fadeCameraAnimator.SetBool ("FadeIn", false);
		fadeCameraAnimator.SetBool ("FadeOut", true);
	}

}
