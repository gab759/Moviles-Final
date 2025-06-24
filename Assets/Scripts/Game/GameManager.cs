using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : SingletonNotPersistent<GameManager>
{

    [Header("Juego")]
    public int vidaAdicionalParaJefe = 0;
    public int puntaje = 0;
    private bool juegoTerminado = false;
    public bool juegoEnPausa { get; private set; } = false;

    [Header("Referencias")]
    public UIManager uiManager;
    public FirestoreService firestoreService;

    [Header("Ranking UI")]  
    [SerializeField] private GameObject panelResults;
    [SerializeField] private TMP_Text[] resultTexts;
    [SerializeField] private ScoreDatabaseSO database;
    public static event System.Action OnSolicitarCerrarSesion;
    private void Start()
    {
        Time.timeScale = 1f;
        ActualizarRankingUI();
    }
    public void FinDePartida()
    {
        if (juegoTerminado) return;
        juegoTerminado = true;
        Time.timeScale = 0f;

        int puntajeMaximoGuardado = PlayerPrefs.GetInt("PuntajeMaximo", 0);
        bool esNuevoRecord = puntaje > puntajeMaximoGuardado;

        if (esNuevoRecord)
        {
            PlayerPrefs.SetInt("PuntajeMaximo", puntaje);
            PlayerPrefs.Save();
        }

        if (firestoreService != null)
        {
            firestoreService.UploadPlayerScore(puntaje);
        }

        uiManager?.MostrarPanelFinDePartida(puntaje, esNuevoRecord);
    }
    public void BotonCerrarSesion()
    {
        OnSolicitarCerrarSesion?.Invoke();
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

    // ======= RANKING UI =======

    public void MostrarResultados()
    {
        panelResults.SetActive(true);
        ActualizarRankingUI();
    }

    private void ActualizarRankingUI()
    {
        for (int i = 0; i < database.topScores.Length; i++)
        {
            var entry = database.topScores[i];
            resultTexts[i].text = $"{i + 1}. {entry.score}";
        }
        firestoreService.UploadPlayerScore(puntaje);
    }

    public void CerrarResultados()
    {
        panelResults.SetActive(false);
    }
}
