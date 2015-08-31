using UnityEngine;
using System.Collections;

public class PelotaGameRules : MonoBehaviour {
	public GameModeVirtual currentGameMode;

	public void OnTriggerEnter (Collider col){
		if (col.transform.tag == "Scorer" && currentGameMode != null)
			currentGameMode.PuntoConseguido();
			//Debug.Log ("GOL");
	}

}
