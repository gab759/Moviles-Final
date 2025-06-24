using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("UI de Menú")]
    [SerializeField] private GameObject panelResults;
    [SerializeField] private TMP_Text[] resultTexts;

    [Header("Datos")]
    [SerializeField] private ScoreDatabaseSO database;

    private void Start()
    {
        UpdateResultsUI();
    }

    // Botón “Resultados”
    public void OnResultsClicked()
    {
        panelResults.SetActive(true);
        UpdateResultsUI();
    }

    // Actualiza visualmente los 5 puntajes
    private void UpdateResultsUI()
    {
        for (int i = 0; i < database.topScores.Length; i++)
        {
            var entry = database.topScores[i];
            resultTexts[i].text = $"{i + 1}. {entry.score}";
        }
    }

    // Botón “Cerrar Resultados”
    public void OnCloseResults()
    {
        panelResults.SetActive(false);
    }
}
