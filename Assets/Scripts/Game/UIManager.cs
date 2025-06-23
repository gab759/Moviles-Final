using UnityEngine;
using TMPro; 

public class UIManager : MonoBehaviour
{
    [Header("Elementos de UI")]
    [SerializeField] private TextMeshProUGUI textoPuntaje;
    [SerializeField] private GameObject panelPausa;
    [SerializeField] private TextMeshPro textoContadorHamburguesas;
    [SerializeField] private GameObject panelFinDePartida;
    [Header("Textos de Fin de Partida")]
    [SerializeField] private TextMeshProUGUI textoPuntajeFinal;
    [SerializeField] private GameObject objetoMensajeNuevoRecord;
    void Update()
    {
        if (textoPuntaje != null)
        {
            textoPuntaje.text = "Puntos: " + GameManager.Instance.puntaje;
        }
    }
    public void MostrarPanelFinDePartida(int puntajeFinal, bool esNuevoRecord)
    {
        panelFinDePartida.SetActive(true); // Mostramos el panel
        textoPuntajeFinal.text = "Puntaje Final: " + puntajeFinal.ToString();

        // Mostramos el mensaje de "¡NUEVO RÉCORD!" solo si es verdad
        objetoMensajeNuevoRecord.SetActive(esNuevoRecord);
    }
    public void ActualizarContadorHamburguesas(int cantidad)
    {
        if (textoContadorHamburguesas != null)
        {
            textoContadorHamburguesas.text = cantidad.ToString();
        }
    }
    public void OnClick_Pausar()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PausarJuego();
            panelPausa.SetActive(true); // Mostramos el panel de pausa
        }
    }
    public void OnClick_Reanudar()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ReanudarJuego();
            panelPausa.SetActive(false); // Ocultamos el panel de pausa
        }
    }
    public void OnClick_Reiniciar()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ReiniciarJuego();
        }
    }
}