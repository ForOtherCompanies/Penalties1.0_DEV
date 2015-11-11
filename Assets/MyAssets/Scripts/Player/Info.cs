using UnityEngine;
using System.Collections;
using System;

public class Info : MonoBehaviour
{

    int[] complementos;
    ArrayList complementosComprados;
    public static Info playerInfo;
    public Info()
    {
        complementos = new int[7];
        complementosComprados = new ArrayList();
    }

    void Awake()
    {
        playerInfo = this;
    }
    public void InsertarComplementoComprado(int id)
    {
        complementosComprados.Add(id);
        int[] array = new int[complementosComprados.Count];
        complementosComprados.CopyTo(array);
        PlayerPrefs.SetString("complementosComprados", string.Join("|", Array.ConvertAll(array, x => x.ToString())));
    }

    public ArrayList getComplementosComprados()
    {
        return complementosComprados = new ArrayList(Array.ConvertAll(PlayerPrefs.GetString("complementosComprados").Split('|'), x => int.Parse(x)));
    }

    public void InsertarComplemento(int id)
    {
        complementos[id % 10] = id;
        PlayerPrefs.SetString("complementos", string.Join("|", Array.ConvertAll(complementos, x => x.ToString())));
    }

    public int[] getComplementos()
    {
        return complementos = Array.ConvertAll(PlayerPrefs.GetString("complementos").Split('|'), x => int.Parse(x));
    }

    public string GetNombre()
    {
        return PlayerPrefs.GetString("nombre");
    }

    public void SetNombre(string nombre)
    {
        PlayerPrefs.GetString("nombre", nombre);
    }

    public void SetMoney(int money)
    {
        PlayerPrefs.SetInt("money", money);
    }
    public int GetMoney()
    {
        return PlayerPrefs.GetInt("money");
    }
    public void SetGold(int gold)
    {
        PlayerPrefs.SetInt("gold", gold);
    }
    public int GetGold()
    {
        return PlayerPrefs.GetInt("gold");
    }
    public string GetPasword()
    {
        return PlayerPrefs.GetString("pasword");
    }

    public void SetPasword(string nombre)
    {
        PlayerPrefs.GetString("pasword", nombre);
    }
}
