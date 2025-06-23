using UnityEngine;

public class DesplazadorDeNivel : MonoBehaviour
{
    public static float velocidadDelJuego = 10f; 

    private Oleada miComponenteOleada;

    void Awake()
    {
        miComponenteOleada = GetComponent<Oleada>();
        if (miComponenteOleada == null)
        {
            Debug.LogError("¡Este objeto de nivel no tiene el componente 'Oleada' y no podrá ser reciclado!", this);
        }
    }

    void Update()
    {
        transform.Translate(Vector3.back * velocidadDelJuego * Time.deltaTime);

        if (transform.position.z < -18f)
        {
           

            if (miComponenteOleada != null && miComponenteOleada.miPoolDeOrigen != null)
            {
                miComponenteOleada.miPoolDeOrigen.ReturnObject(miComponenteOleada);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}