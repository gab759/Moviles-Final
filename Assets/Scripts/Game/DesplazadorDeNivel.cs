using UnityEngine;

public class DesplazadorDeNivel : MonoBehaviour
{
   
    public static float velocidadDelJuego = 3f;

    void Update()
    {
        transform.Translate(Vector3.back * velocidadDelJuego * Time.deltaTime);

        if (transform.position.z < -10f) 
        {
            Destroy(gameObject); 
        }
    }
}