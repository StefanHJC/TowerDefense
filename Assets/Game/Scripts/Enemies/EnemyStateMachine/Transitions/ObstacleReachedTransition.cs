using UnityEngine;

public class ObstacleReachedTransition : Transition
{
    private void Start()
    {
        Enemy.ObstacleReached += OnObstacleReached;
    }

    private void OnDestroy()
    {
        Enemy.ObstacleReached -= OnObstacleReached;
    }

    private void OnObstacleReached(Obstacle obstacle)
    {
        NeedTransit = true;
    }
}
