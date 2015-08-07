using UnityEngine;
using System.Collections;

public class InputForDebug : MonoBehaviour {

	public IATiro iaTiro;



	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("space")){
			iaTiro.Tirar();
		}
	}
}
