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
        float minX = MinX;
        float maxX = MaxX;
        /*
        switch (Random.Range(0, 3))
        {
            case 0:
                minX = 0;
                break;
            case 1:
                maxX = 0;
                break;

        }
         * */
        x = Random.Range(minX, maxX);
        y = Random.Range(MinY, MaxY);
        direccion = new Vector3(x, y, 0);
        //direccion =impacto - Posicion.transform.position ;
        //direccion = direccion.normalized;
       // direccion.y *= 5f;
        direccion.z = 1;
        direccion.y = Mathf.Clamp(direccion.y, 0f, 0.7f);
        direccion.x = Mathf.Clamp(direccion.y, -0.7f, 0.7f);
        Debug.Log("vector IA tiro " + direccion);
        fuerza = Random.Range(MinForce, MaxForce);
        fisicasIA.AccionIA(direccion, fuerza, level);
        //level = Mathf.Clamp(level,0,8)+1;
    }
}
