using UnityEngine;
using System.Collections;

public class IATiro : IAManager {


	void Start () {
		level = 0;
		MaxForce = 190f;
		MinForce = 175f;
	}

	public override void RealizarAccion ()
	{
		x = Random.Range(MinX,MaxX);
		y = Random.Range(MinY,MaxY);
		impacto = new Vector3(x,y,Z);
		direccion =impacto - Posicion.transform.position ;
		direccion= direccion.normalized;
		direccion.y*=2.68f;
        direccion.z *= -1;
		fuerza = Random.Range(MinForce,MaxForce);
		fisicasIA.AccionIA(direccion,fuerza,level);
		//level = Mathf.Clamp(level,0,8)+1;
	}
}
