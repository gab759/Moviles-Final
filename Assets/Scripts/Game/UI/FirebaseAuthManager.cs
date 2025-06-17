using UnityEngine;
using Firebase;
using Firebase.Auth;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
public class FirebaseAuthManager : MonoBehaviour
{
    private FirebaseAuth auth;
    private FirebaseUser user;

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
                return;
            }

            user = task.Result.User;
            Debug.Log("Cuenta creada con éxito: " + user.Email);
        });
    }

    public void LoginConCorreo(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Error al iniciar sesión: " + task.Exception.InnerExceptions[0].Message);
                return;
            }

            user = task.Result.User;
            Debug.Log("Sesión iniciada como: " + user.Email);

            // Solo si todo fue bien
            SceneManager.LoadScene("Game");

        }, TaskScheduler.FromCurrentSynchronizationContext());
    }
}
