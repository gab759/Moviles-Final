using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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

    [SerializeField] private FirebaseAuthManager authManager;

    private void Start()
    {
        authManager = GetComponent<FirebaseAuthManager>();

        // Mostrar solo el panel de inicio al comenzar
        panelInicio.SetActive(true);
        panelLogin.SetActive(false);
        panelRegistro.SetActive(false);
    }
    public void OnClickInvitado()
    {
        SceneManager.LoadScene("Game");
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
}
