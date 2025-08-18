using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger Instance { get; private set; }

    public event Action<float> OnLoadingProgress;
    public event Action OnLoadingFinished;


    private void OnEnable()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void OnDisable()
    {
        Instance = null;
    }

    public void ReloadCurrentScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneAsyncRoutine(sceneName));
    }

    private IEnumerator LoadSceneAsyncRoutine(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float maxProgress = 0.9f;
            float progress = Mathf.Clamp01(operation.progress / maxProgress);

            OnLoadingProgress?.Invoke(progress);

            if (operation.progress >= maxProgress)
                operation.allowSceneActivation = true;

            yield return null;
        }

        OnLoadingFinished?.Invoke();
    }
}