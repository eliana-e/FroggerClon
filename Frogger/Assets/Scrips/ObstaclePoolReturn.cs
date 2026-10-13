using UnityEngine;

public class ObstaclePoolReturn : MonoBehaviour
{
    [SerializeField] private int size = 1;

    private ObstaclePool _pool;
    private Vector3 _leftEdge;
    private Vector3 _rightEdge;
    private Vector2 _direction;
    
    public void Initialize(ObstaclePool pool)
    {
        _pool = pool;
        _leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        _rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    private void Update()
    {
        if (_direction.x > 0 && (transform.position.x - size) > _rightEdge.x)
            _pool.ReturnObstacle(gameObject);

        else if (_direction.x < 0 && (transform.position.x + size) < _leftEdge.x)
            _pool.ReturnObstacle(gameObject);
    }
}
