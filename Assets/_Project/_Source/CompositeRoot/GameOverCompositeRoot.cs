using UnityEngine;

public class GameOverCompositeRoot : CompositeRoot
{
    [SerializeField] private SceneReference _sceneReference;
    [SerializeField] private CollisionProcessorCompositeRoot _collisionProcessor;

    private CollisonRecords _records;
    private SceneChanger _changer;

    private bool _isGameOver;

    private void OnDisable()
    {
        _records.GameStopped -= ChangeScene;
    }

    private void OnValidate()
    {
        _sceneReference.OnValidate();
    }

    public override void Composite()
    {
        _records = _collisionProcessor.Records;
        _changer = SceneChanger.Instance;
        _records.GameStopped += ChangeScene;
    }

    private void ChangeScene()
    {
        if (_isGameOver)
            return;

        _changer.LoadSceneAsync(_sceneReference.SceneName);

        _isGameOver = true;
    }
}
