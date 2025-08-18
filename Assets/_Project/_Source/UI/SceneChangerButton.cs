using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneChangerButton : MonoBehaviour
{
    [SerializeField] private SceneReference _nextScene;

    private SceneChanger _sceneChanger;

    private void OnValidate()
    {
        _nextScene.OnValidate();
    }

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(ChangeScene);
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(ChangeScene);
    }

    private void Start()
    {
        _sceneChanger = SceneChanger.Instance;
    }

    public void ChangeScene()
    {
        _sceneChanger.LoadSceneAsync(_nextScene.SceneName);
    }
}