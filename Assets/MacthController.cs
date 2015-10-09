using UnityEngine;
using System.Collections;

public class MacthController : MonoBehaviour {

    ModoJuego modalidadActivada = null;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
        modalidadActivada.Update();
	}


     public void ActivarModoActual(ModoJuego modo)
     {
         modalidadActivada = modo;
     }

}
