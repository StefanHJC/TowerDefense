
public class ObstacleDestroyedTransition : Transition
{
    private void Start()
    {
        Enemy.TargetDestroyed += OnObstacleDestroyed;
    }

    private void OnDestroy()
    {
        //Enemy.TargetDestroyed -= OnObstacleDestroyed;
    }

    private void OnObstacleDestroyed()
    {
        NeedTransit = true;
    }
}
