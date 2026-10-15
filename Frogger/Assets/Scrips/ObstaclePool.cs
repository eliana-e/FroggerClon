using UnityEngine;
using UnityEngine.Pool;

public class ObstaclePool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int capacity = 4;
    [SerializeField] private int maxSize = 10;

    private ObjectPool<GameObject> _pool;
    void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: CreateObstacle,
            actionOnGet: OnGetFromPool,
            actionOnRelease: OnReturnToPool,
            actionOnDestroy: OnDestroyObstacle,
            collectionCheck: true,
            defaultCapacity: capacity,
            maxSize: maxSize
        );
    }

    // Cuando el pool necesita crear un obstaculo nuevo
    private GameObject CreateObstacle()
    {
        GameObject obj = Instantiate(prefab);
        obj.GetComponent<ObstaclePoolReturn>().Initialize(this);
        return obj;
    }

    // Cuando se saca del pool
    private void OnGetFromPool(GameObject obj)
    {
        obj.SetActive(true);
    }

    // Cuando se devuelve al pool
    private void OnReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    // Si el pool está lleno y se descarta
    private void OnDestroyObstacle(GameObject obj)
    {
        Destroy(obj);
    }

    public GameObject GetObstacle() => _pool.Get();

    public void ReturnObstacle(GameObject obj) => _pool.Release(obj);

}
