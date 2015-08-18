using UnityEngine;
using System.Collections;

public class CategoriaComplemento : MonoBehaviour {

	//keep public
	public GameObject boneParent;
	public GameObject positionHelper;
	public GameObject complementoActivo;//para mantener en cada categoria apuntado su complemento si es que esta puesto
	//public arraylist de botones-objeto (cada boton podria ser un 'prefab' que ya incluya el modelo y todo; el modelo estaria desactivado hasta que se seleccione y se coloque en su sitio
	////cada prefab tipo 'complemento' debe colgar de un holder para mover el holder a la posicion del helper y luego recolocar el objeto en su
	////sitio desde unity. OJO con la normalizacion de posiciones que puede ser un jaleo

}
