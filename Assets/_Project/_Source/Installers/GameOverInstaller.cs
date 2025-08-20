using UnityEngine;

public class GameOverInstaller : Installer
{
    [SerializeField] private SceneReference _sceneReference;
    [SerializeField] private CollisionProcessorInstaller _collisionProcessor;
    [SerializeField] private ServicesInstaller _servicesInstaller;

    private CollisonRecords _records;
    private SceneChanger _changer;

    private bool _isGameOver = false;

    private void OnDisable()
    {
        _records.GameStopped -= ChangeScene;
    }

    private void OnValidate()
    {
        _sceneReference.OnValidate();
    }

    public override void Install()
    {
        _records = _collisionProcessor.Records;
        _changer = SceneChanger.Instance;
        _records.GameStopped += ChangeScene;
    }

    private void ChangeScene()
    {
        _servicesInstaller.Dispose.Dispose();

        if (_isGameOver)
            throw new System.InvalidOperationException(nameof(ChangeScene));

        _changer.LoadSceneAsync(_sceneReference.SceneName);

        _isGameOver = true;
    }
}
