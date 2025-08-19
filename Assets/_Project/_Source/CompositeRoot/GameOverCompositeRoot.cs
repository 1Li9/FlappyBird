using UnityEngine;

public class GameOverCompositeRoot : CompositeRoot
{
    [SerializeField] private SceneReference _sceneReference;
    [SerializeField] private CollisionProcessorCompositeRoot _collisionProcessor;
    [SerializeField] private ServicesCompositeRoot _servicesRoot;

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
        _servicesRoot.Dispose.Dispose();

        if (_isGameOver)
            throw new System.InvalidOperationException(nameof(ChangeScene));

        _changer.LoadSceneAsync(_sceneReference.SceneName);

        _isGameOver = true;
    }
}
