using UnityEngine;
using System.Collections;

public class IATiro : IAManager
{


    void Start()
    {
        level = 0;
        MaxForce = 180f;
        MinForce = 170f;
    }

    public override void RealizarAccion()
    {
        x = Random.Range(MinX, MaxX);
        y = Random.Range(MinY, MaxY);
        direccion = new Vector3(x, y, 0);
        //direccion =impacto - Posicion.transform.position ;
        //direccion = direccion.normalized;
       // direccion.y *= 5f;

        direccion.Normalize();
        direccion.z = 1;
        direccion.x = Random.Range(-0.6f, 0.6f);
        Debug.Log("vector IA tiro " + direccion);
        fuerza = Random.Range(MinForce, MaxForce);
        fisicasIA.AccionIA(direccion, fuerza, level);
        //level = Mathf.Clamp(level,0,8)+1;
    }
}
