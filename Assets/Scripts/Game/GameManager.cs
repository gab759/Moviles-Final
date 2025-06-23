using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int vidaAdicionalParaJefe = 0;
    public int puntaje = 0;
    public UIManager uiManager;
    private bool juegoTerminado = false;
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
    public void FinDePartida()
    {
        if (juegoTerminado) return;
        juegoTerminado = true;
        Time.timeScale = 0f;

        // 1. Cargamos el puntaje máximo guardado localmente.
        // La llave es "PuntajeMaximo". Si no existe, por defecto nos dará 0.
        int puntajeMaximoGuardado = PlayerPrefs.GetInt("PuntajeMaximo", 0);

        // 2. Comparamos el puntaje de esta partida con el máximo guardado.
        bool esNuevoRecord = puntaje > puntajeMaximoGuardado;

        if (esNuevoRecord)
        {
            // 3. Si hay un nuevo récord, lo guardamos en el dispositivo con la misma llave.
            Debug.Log("¡Nuevo Récord Local! Guardando puntaje: " + puntaje);
            PlayerPrefs.SetInt("PuntajeMaximo", puntaje);
            PlayerPrefs.Save(); // Forzamos el guardado inmediato en el disco.
        }

        // 4. Le pedimos al UIManager que muestre el panel final.
        // El UIManager no necesita saber de dónde vino la información, solo la muestra.
        if (uiManager != null)
        {
            uiManager.MostrarPanelFinDePartida(puntaje, esNuevoRecord);
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