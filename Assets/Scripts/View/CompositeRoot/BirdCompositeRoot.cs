using UnityEngine;

public class BirdCompositeRoot : MonoBehaviour
{
    [SerializeField] private BirdConfig _birdConfig;
    [SerializeField] private KeyboardInput _input;

    private EntityView _view;

    public EntityView CreateBirdView()
    {
        BirdFabric birdFabric = new(_birdConfig, _input);
        _view = Instantiate(_birdConfig.View);

        Bird bird = birdFabric.Create();
        _view.Bind(bird);

        return _view;
    }
}