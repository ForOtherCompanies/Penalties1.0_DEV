using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

//	public bool portero = true;

	//to set private
	public Vector2 inicioTouch = Vector2.zero;
	public Vector2 finTouch = Vector2.zero;

	private Touch myTouch; 

	//references to other scripts
	//public GameManager gameManager;
	public GameModeVirtual gameManager;
	public InputEffects inputEffects;
	
	// Update is called once per frame
	void Update () {



		if (Input.touchCount>0){
			myTouch = Input.GetTouch (0);
			//al inicio del touch guardar la posicion
			if (myTouch.phase ==TouchPhase.Began){
				inicioTouch = myTouch.position;

			}

			//al final del touch guaradr la posicion y llamar a la funcion que calcula el tiro pasando los parametros
			if (myTouch.phase == TouchPhase.Ended){
				finTouch = myTouch.position;	
				gameManager.RealizarAcciones (inicioTouch, finTouch);		
				inputEffects.Parar();
			}
		}

	}

}
