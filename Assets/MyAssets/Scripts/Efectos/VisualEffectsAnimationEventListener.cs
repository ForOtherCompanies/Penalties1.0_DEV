using UnityEngine;
using System.Collections;

public class VisualEffectsAnimationEventListener : MonoBehaviour {
	public CameraEffects cameraEffects;


	public void ResetCicloFadeInOutFlag(){
		cameraEffects.ResetCicloFlag();
		cameraEffects.fadeCameraAnimator.SetBool ("CicloFadeOutIn", false);
	}
}
