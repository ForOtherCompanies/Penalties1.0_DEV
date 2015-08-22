using UnityEngine;
using System.Collections;

public class CategoriaComplemento : MonoBehaviour {

	//keep public
	public string nombre;

	public GameObject boneParent;
	public GameObject positionHelper;

	public GameObject buttonsHolder;

	public GameObject [] buttonsHelpers;
	public GameObject complementoActivo;//para mantener en cada categoria apuntado su complemento si es que esta puesto
	public GameObject [] buttons; //public arraylist de botones-objeto (cada boton podria ser un 'prefab' que ya incluya el modelo y todo; el modelo estaria desactivado hasta que se seleccione y se coloque en su sitio (esto ya no es necesario)
	////cada prefab tipo 'complemento' debe colgar de un holder para mover el holder a la posicion del helper y luego recolocar el objeto en su
	////sitio desde unity. OJO con la normalizacion de posiciones que puede ser un jaleo



	public void Start (){

		for (int i = 0; i < buttonsHelpers.Length; i++){
			//Debug.Log ("instancio " + button.name);
			GameObject newButton = Instantiate (buttons[i]);
			newButton.transform.SetParent(buttonsHolder.transform, false);
			newButton.transform.position = buttonsHelpers[i%buttonsHelpers.Length].transform.position;
		}
		//primero se instancian todos los botones y se colocan en su sitio y tal.


		////consultar a la base de datos los objetos disponibles para este jugador y los que sean button.categoria == this.nombre se marcan como
		////activos. Al pulsarse un boton inactivo este decide si ativarse o no previa consulta a la base de datos y tal
	}
}
