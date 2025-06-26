// Moneda.cs
using UnityEngine;

public class Moneda : PoolObject // Hereda de PoolObject por si se poolea en el futuro
{
    public int valor = 10;

    void Update()
    {
        transform.Rotate(Vector3.up, 90f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hamburguesa"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AnadirPuntos(valor);
            }
            gameObject.SetActive(false); // Simplemente se desactiva. La Oleada la reactivará.
        }
        if (other.CompareTag("PlayerL"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AnadirPuntos(valor);
            }
            gameObject.SetActive(false); // Simplemente se desactiva. La Oleada la reactivará.
        }
    }
}

public class MonedaPool : DynamicObjectPooling<Moneda> { }