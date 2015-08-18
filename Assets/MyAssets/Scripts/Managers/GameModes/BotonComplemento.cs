﻿using UnityEngine;
using System.Collections;

public class BotonComplemento : MonoBehaviour {

	//keep public
	public GameObject complementoAsociado; //puntero al prefab del complemento asociado del boton (necesario para instanciar y colocar cuando se pulse)
	public CategoriaComplemento categoria; //referencia a la categoria a la que pertenece este boton (y, por tanto, la forma de encontrar el helper donde colocar el objeto)
	//para comprobar si el objeto esta ya comprado o no


	//set private
	//para comprobar que hacer si se pulsa el boton (si esta comprado 'ponerlo' y si no pues ir a las opciones de compr
	public bool comprado = false;
	public bool yaPuesto = false;




	public void PerformActions (){
		if (comprado && !yaPuesto){
			Destroy (categoria.complementoActivo);
			categoria.complementoActivo =(GameObject) Instantiate (complementoAsociado, categoria.positionHelper.transform.position, categoria.positionHelper.transform.rotation);
			categoria.complementoActivo.transform.parent = categoria.boneParent.transform;//o quiza rootearlo en el helper en lugar de en el bone
		}//else pasar a comprar

	}

}
