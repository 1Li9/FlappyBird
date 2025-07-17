using UnityEngine;

public class GameView : MonoBehaviour
{
    private Game _game;
    private CollisionRouter _collisionRouter;

    public void Initialize(Game game, CollisionRouter collisionRouter)
    {
        _game = game;
        _collisionRouter = collisionRouter;
    }

    private void Update()
    {
        _collisionRouter.ProcessCollisions();
        _game.Tick(Time.deltaTime);
    }
}