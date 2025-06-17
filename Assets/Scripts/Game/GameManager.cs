using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int vidaAdicionalParaJefe = 0;
    public int puntaje = 0;
    public bool juegoEnPausa { get; private set; } = false;

    private void Awake()
    {
        Time.timeScale = 1f;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            
        }
    }
    public void AnadirPuntos(int puntosAGanar)
    {
        puntaje += puntosAGanar;
    }
    public void PausarJuego()
    {
        juegoEnPausa = true;
        Time.timeScale = 0f;
    }
    public void ReanudarJuego()
    {
        juegoEnPausa = false;
        Time.timeScale = 1f; 
    }
    public void ReiniciarJuego()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("game2");
    }
}