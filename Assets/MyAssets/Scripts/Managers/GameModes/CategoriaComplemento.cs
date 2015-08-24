using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CategoriaComplemento : MonoBehaviour {

	//keep public
	public string nombre;

	public GameObject boneParent;
	public GameObject positionHelper;
	public Texture prueba;

	public GameObject buttonsHolder;//donde se guardaran los botones
	public int numBotones;//num de elemtos que contendra el prefab

	public GameObject button;//prefab de boton generico
	public GameObject [] buttonsHelpers;//sirven para colocar los elementos en su lugar correspondiente
	public GameObject complementoActivo;//para mantener en cada categoria apuntado su complemento si es que esta puesto
	public GameObject [] objetos; //public arraylist de objetos (estos seran los punteros a los elemtos para colocar en el player)
	////cada prefab tipo 'complemento' debe colgar de un holder para mover el holder a la posicion del helper y luego recolocar el objeto en su
	////sitio desde unity. OJO con la normalizacion de posiciones que puede ser un jaleo



	public void Start (){
		float incrementoX = buttonsHelpers [1].transform.position.x - buttonsHelpers [0].transform.position.x;
		float incrementoY = buttonsHelpers [2].transform.position.y - buttonsHelpers [0].transform.position.y; 
		Vector3 posicionActual = buttonsHelpers [0].transform.position;

		for (int i = 0; i < numBotones; i++){
			Debug.Log ("instancio " + "button");
			GameObject newButton = Instantiate (button);
			newButton.transform.SetParent(buttonsHolder.transform, false);
			newButton.transform.position = posicionActual;
			Image imagen = newButton.GetComponent<Image>();
			//imagen.material.mainTexture = prueba;

			if((i+1)%3==0){
				posicionActual.y+=incrementoY;
				posicionActual.x-=2*incrementoX;
			}else{
				posicionActual.x+=incrementoX;
			}
		}
		//primero se instancian todos los botones y se colocan en su sitio y tal.


		////consultar a la base de datos los objetos disponibles para este jugador y los que sean button.categoria == this.nombre se marcan como
		////activos. Al pulsarse un boton inactivo este decide si ativarse o no previa consulta a la base de datos y tal
	}
}
