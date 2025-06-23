using UnityEngine;
using Firebase;
using Firebase.Auth;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
public class FirebaseAuthManager : MonoBehaviour
{
    private FirebaseAuth auth;
    private FirebaseUser user;
    public AuthUIManager uiManager;
    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;
                Debug.Log("Firebase Auth inicializado correctamente.");
            }
            else
            {
                Debug.LogError("Firebase no está disponible: " + task.Result);
                uiManager.MostrarMensaje("Error al iniciar Firebase");
            }
        });
    }

    public void RegistrarCuenta(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Error al crear cuenta: " + task.Exception);
                uiManager.MostrarMensaje("Error al crear cuenta");
                return;
            }

            user = task.Result.User;
            Debug.Log("Cuenta creada con éxito: " + user.Email);
            uiManager.MostrarMensaje("Cuenta creada correctamente");
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    public void LoginConCorreo(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Error al iniciar sesión: " + task.Exception);
                uiManager.MostrarMensaje("Error al iniciar sesión");
                return;
            }

            user = task.Result.User;
            Debug.Log("Sesión iniciada como: " + user.Email);

            SceneManager.LoadScene("game2");
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }
}