using UnityEngine;

public class LaneSpawner : MonoBehaviour
{
    [Header("Referencia")]
    [SerializeField] private ObstaclePool pool;

    [Header("Carril")]
    [SerializeField] private float laneY = 0f;
    [SerializeField] private bool movesRight = true;

    [Header("Dificultad")]
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float obstacleSpeed = 3f;

    private float _timer;
    private Vector3 _leftEdge;
    private Vector3 _rightEdge;
    private void Start()
    {
        _leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        _rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        // Arranca el timer con un valor random para que los carriles
        // no spaween todos al mismo tiempo
        _timer = Random.Range(0f, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= spawnInterval)
        {
            _timer = 0f;
            SpawnObstacle();
        }
    }

    private void SpawnObstacle()
    {
        GameObject obstacle = pool.GetObstacle();

        // Spawneá justo fuera del borde según la dirección
        float spawnX = movesRight ? _leftEdge.x - 1f : _rightEdge.x + 1f;
        obstacle.transform.position = new Vector3(spawnX, laneY, 0f);

        // Configurá movimiento
        var movement = obstacle.GetComponent<ObstacleMovement>();
        Vector2 dir = movesRight ? Vector2.right : Vector2.left;
        movement.SetDirection(dir);
        movement.SetSpeed(obstacleSpeed);

        // Avisale al PoolReturn en qué dirección va para el despawn
        var poolReturn = obstacle.GetComponent<ObstaclePoolReturn>();
        poolReturn.SetDirection(dir);
    }
}
