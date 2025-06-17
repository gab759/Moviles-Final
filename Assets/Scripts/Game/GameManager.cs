using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            
        }
    }


    
    public int vidaAdicionalParaJefe = 0;
    public int puntaje = 0;

    public void AnadirPuntos(int puntosAGanar)
    {
        puntaje += puntosAGanar;
    }
}