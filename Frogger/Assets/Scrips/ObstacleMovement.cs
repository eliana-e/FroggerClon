using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] private Vector2 direction = Vector2.right;
    [SerializeField] private float speed = 1f;

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    // El LaneSpawner los configura al sacarlos del pool
    public void SetDirection(Vector2 direction) => this.direction = direction;
    public void SetSpeed(float speed) => this.speed = speed;
}
