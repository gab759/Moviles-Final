using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class AuthUIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject panelInicio;
    public GameObject panelLogin;
    public GameObject panelRegistro;

    [Header("Login Inputs")]
    public TMP_InputField loginEmailInput;
    public TMP_InputField loginPasswordInput;

    [Header("Registro Inputs")]
    public TMP_InputField registroEmailInput;
    public TMP_InputField registroPasswordInput;

    [Header("Mensajes")]
    public TextMeshProUGUI mensajeTexto;

    [SerializeField] private FirebaseAuthManager authManager;

    private Coroutine mensajeCoroutine;

    private void Start()
    {
        authManager = GetComponent<FirebaseAuthManager>();
        authManager.uiManager = this;

        panelInicio.SetActive(true);
        panelLogin.SetActive(false);
        panelRegistro.SetActive(false);
        mensajeTexto.text = "";
        mensajeTexto.alpha = 1f;
    }

    public void OnClickInvitado()
    {
        SceneManager.LoadScene("game2");
    }

    public void OnClickLogin()
    {
        string email = loginEmailInput.text;
        string password = loginPasswordInput.text;

        authManager.LoginConCorreo(email, password);
    }

    public void OnClickRegistro()
    {
        string email = registroEmailInput.text;
        string password = registroPasswordInput.text;

        authManager.RegistrarCuenta(email, password);
    }

    public void MostrarMensaje(string mensaje)
    {
        mensajeTexto.text = mensaje;
        mensajeTexto.alpha = 1f;

        if (mensajeCoroutine != null)
            StopCoroutine(mensajeCoroutine);

        mensajeCoroutine = StartCoroutine(FadeOutMensaje());
    }

    private IEnumerator FadeOutMensaje()
    {
        float duration = 2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            mensajeTexto.alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            yield return null;
        }

        mensajeTexto.text = "";
        mensajeTexto.alpha = 1f;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
