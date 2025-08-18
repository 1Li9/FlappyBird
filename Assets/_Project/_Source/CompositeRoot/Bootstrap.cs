using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private CompositeRoot[] _roots;

    private PauseService _pauseService;
    private GameControlls _inputs;

    private void OnEnable()
    {
        _inputs = new GameControlls();
        _inputs.Enable();
        _inputs.Game.Pause.performed += TogglePause;
        _inputs.Game.Reload.performed += ReloadScene;
    }

    private void Awake()
    {
        foreach (var root in _roots)
        {
            root.Composite();

            if (root.gameObject.TryGetComponent(out ServicesCompositeRoot servicesRoot))
                _pauseService = servicesRoot.Pause;
        }
    }

    private void OnDisable()
    {
        _inputs.Disable();
        _inputs.Game.Pause.performed -= TogglePause;
        _inputs.Game.Reload.performed -= ReloadScene;
    }

    private void TogglePause(InputAction.CallbackContext obj)
    {
        if (_pauseService.IsPaused)
            _pauseService.Play();
        else
            _pauseService.Pause();
    }

    private void ReloadScene(InputAction.CallbackContext obj)
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync(scene.buildIndex);
    }
}