using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class GlobalSceneManager : MonoBehaviour
{
    public SceneDataSO sceneData;

    public static event Action<string> OnSceneLoaded;
    public static event Action<string> OnSceneUnloaded;
    public static event Action<string> OnSceneLoadStarted;
    public static event Action<string> OnSceneUnloadStarted;

    public void LoadSceneAdditive(string sceneName)
    {
        if (!IsSceneValid(sceneName)) return;

        OnSceneLoadStarted?.Invoke(sceneName);
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    public void UnloadScene(string sceneName)
    {
        if (!IsSceneValid(sceneName)) return;

        OnSceneUnloadStarted?.Invoke(sceneName);
        StartCoroutine(UnloadSceneAsync(sceneName));
    }

    public void UnloadAllScenesExcept(string exceptScene)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != exceptScene)
            {
                UnloadScene(scene.name);
            }
        }
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        yield return new WaitUntil(() => op.isDone);
        OnSceneLoaded?.Invoke(sceneName);
    }

    private IEnumerator UnloadSceneAsync(string sceneName)
    {
        AsyncOperation op = SceneManager.UnloadSceneAsync(sceneName);
        yield return new WaitUntil(() => op.isDone);
        OnSceneUnloaded?.Invoke(sceneName);
    }

    private bool IsSceneValid(string sceneName)
    {
        if (sceneData == null) return true;
        foreach (string validScene in sceneData.scenes)
        {
            if (validScene == sceneName) return true;
        }
        Debug.LogWarning("Escena " + sceneName + " no está listada en SceneDataSO.");
        return false;
    }
}