using UnityEngine;
using UnityEditor;

[System.Serializable]
public class SceneReference
{
    [SerializeField] private SceneAsset sceneAsset;

    public string SceneName { get; private set; }

    public void OnValidate()
    {
        if (sceneAsset != null)
            SceneName = sceneAsset.name;
    }
}