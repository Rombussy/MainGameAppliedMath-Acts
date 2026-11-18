using Unity.VisualScripting;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] Obstacles obstaclePrefab;
    [SerializeField] Transform[] _lanes;
    [SerializeField] float spawnInterval = 1f;
    [SerializeField] PlayerControl _player;
    private float timer;

    private void Update()
    {
        if (obstaclePrefab == null || _lanes == null || _lanes.Length == 0)
            return;
        timer += Time.deltaTime;

        if(timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    private void SpawnObstacle()
    {
        int randomLane = Random.Range(0, _lanes.Length);
        Transform chosenLane = _lanes[randomLane];
        Obstacles newObstacles = Instantiate(obstaclePrefab);
        newObstacles.laneIndex = randomLane;
        newObstacles.player = _player;

        newObstacles.obstaclePosition = new Vector3(chosenLane.position.x, chosenLane.position.y, newObstacles.obstaclePosition.z);
        newObstacles.Perspective();
    }
}
