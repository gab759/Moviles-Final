using UnityEngine;
using System.Collections.Generic; 
public class Oleada : PoolObject
{
    public OleadaPool miPoolDeOrigen;

    private List<Portal> listaDePortales;
    private List<Moneda> listaDeMonedas;


    void Awake()
    {
        listaDePortales = new List<Portal>(GetComponentsInChildren<Portal>(true));
        listaDeMonedas = new List<Moneda>(GetComponentsInChildren<Moneda>(true));

    }

    void OnEnable()
    {
        ResetearOleada();
    }

    private void ResetearOleada()
    {
        for (int i = 0; i < listaDePortales.Count; i++)
        {
            Portal portal = listaDePortales[i];
            portal.gameObject.SetActive(true);
            portal.ConfigurarPortal();
        }
        for (int i = 0; i < listaDeMonedas.Count; i++)
        {
            // Simplemente reactivamos la moneda. Su script se encargará del resto.
            listaDeMonedas[i].gameObject.SetActive(true);
        }
    }
}
