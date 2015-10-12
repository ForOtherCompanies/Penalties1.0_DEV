using UnityEngine;
using System.Collections;

public class Info : MonoBehaviour {

    private string nombre;
    int[] complementos;
    private Info()
    {
        complementos = new int[7];
    }

    //el nombre del complemento sera ese ID
    public void InsertarComplemento(int id)
    {
        complementos[id % 10] = id;
    }
    public string GetNombre()
    {
        return nombre;
    }

    public void SetNombre(string _nombre)
    {
       nombre= _nombre;
    }

}
