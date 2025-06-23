using UnityEngine;

public class GeneradorDeHamburguesas : MonoBehaviour
{
    [SerializeField] private HamburguesaPool hamburguesaPool;

    public Hamburguesa GenerarHamburguesa(Vector3 posicion)
    {
        Hamburguesa nuevaHamburguesa = hamburguesaPool.GetObject();

       
        if (nuevaHamburguesa == null)
        {
            return null;
        }

        nuevaHamburguesa.transform.position = posicion;
        nuevaHamburguesa.transform.rotation = Quaternion.identity;

        return nuevaHamburguesa;
    }
}