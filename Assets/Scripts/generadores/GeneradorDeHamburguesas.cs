using UnityEngine;

public class GeneradorDeHamburguesas : MonoBehaviour
{
    [SerializeField] private HamburguesaPool hamburguesaPool;


    public Hamburguesa GenerarHamburguesa(Vector3 posicion)
    {
        // Pedimos una hamburguesa al pool.
        Hamburguesa nuevaHamburguesa = hamburguesaPool.GetObject();

        // La colocamos en la posici�n deseada y con rotaci�n inicial.
        nuevaHamburguesa.transform.position = posicion;
        nuevaHamburguesa.transform.rotation = Quaternion.identity;

        return nuevaHamburguesa;
    }
}