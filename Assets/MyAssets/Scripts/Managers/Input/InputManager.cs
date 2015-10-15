using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

//	public bool portero = true;

	//to set private
	//public Vector2 inicioTouch = Vector2.zero;
	//public Vector2 finTouch = Vector2.zero;

	//private Touch myTouch; 

	//references to other scripts
	//public GameManager gameManager;
	public InputEffects inputEffects;
	
	// Update is called once per frame
	/*void Update () {

		if (Input.touchCount>0){
			myTouch = Input.GetTouch (0);

			//al inicio del touch guardar la posicion
			if (myTouch.phase ==TouchPhase.Began){
				inicioTouch = myTouch.position;
				inputEffects.iniciar();
			}

			//al final del touch guardar la posicion y llamar a la funcion que calcula el tiro pasando los parametros
			if (myTouch.phase == TouchPhase.Ended){
				finTouch = myTouch.position;	
				gameManager.RealizarAcciones (inicioTouch, finTouch);		
				inputEffects.Parar();
			}
		}
    }*/
     private float length = 0;
     private bool SW = false;
     private Vector3 final;
     private Vector3 startpos;

     private Vector3 inicio;
     private Vector3 endpos;
     
     // Update is called once per frame
     void Update ()
     {
     
         if (Input.touchCount>0 && Input.GetTouch (0).phase == TouchPhase.Began) 
         {
             final = Vector3.zero;
             length = 0;
             SW = false;
             Vector2 touchDeltaPosition = Input.GetTouch (0).position;
             startpos = new Vector3 (touchDeltaPosition.x, 0, touchDeltaPosition.y);
             inicio = startpos;
             inputEffects.iniciar();
         }
         if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) 
         {
             SW = true;
             Vector2 touchPosition = Input.GetTouch(0).position;
             endpos = new Vector3(touchPosition.x, 0, touchPosition.y);
             final = endpos - startpos;
             length += final.magnitude;
             startpos = endpos;
         }

         if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Canceled) 
         {
         }

         if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary) 
         {
         }
         if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) 
         {
             if (SW) 
             {

                 Debug.Log("parece que funciona");
                 Vector2 touchPosition = Input.GetTouch (0).position;
                 endpos = new Vector3 (touchPosition.x, touchPosition.y,0 );
                 Debug.Log(endpos);
                 final = endpos - startpos;
                 length += final.magnitude;

                 this.GetComponent<MacthController>().RealizarAcciones(length, endpos,inicio);
                 inputEffects.Parar();
             }
             else
             {
                 Debug.Log("error en el input");
             }
         }
     }

}
