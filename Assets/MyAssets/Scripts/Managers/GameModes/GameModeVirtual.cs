using UnityEngine;
using System.Collections;

public class GameModeVirtual : MonoBehaviour {
	public CameraEffects cameraEffects;

	virtual public void RealizarAcciones (Vector2 inicioTouch, Vector3 destinoTouch)
	{
	}

	virtual public void PuntoConseguido () {

		Debug.Log ("GOL SUMADO");
	}
}
