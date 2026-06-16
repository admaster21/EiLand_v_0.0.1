using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public GameObject treePrefab;
    public GameObject rockPrefab;
    public GameObject fiberPrefab;

    public int treeAmount = 30;
    public int rockAmount = 15;
    public int fiberAmount = 20;

    public Vector2 spawnAreaSize = new Vector2(60f, 60f);
    //public float spawnHeight = 1f;
    public float treeSpawnHeight = 2f;
    public float rockSpawnHeight = 0f;
    public float fiberSpawnHeight = 0f;
    public Vector2 treeScaleRange = new Vector2(0.8f, 1.3f);
    public Vector2 rockScaleRange = new Vector2(0.8f, 1.4f);
    public Vector2 fiberScaleRange = new Vector2(0.8f, 1.2f);

    void Start()
    {
        SpawnResource(treePrefab, treeAmount, treeScaleRange, treeSpawnHeight);
        SpawnResource(rockPrefab, rockAmount, rockScaleRange, rockSpawnHeight);
        SpawnResource(fiberPrefab, fiberAmount, fiberScaleRange, fiberSpawnHeight);
    }

    void SpawnResource(GameObject prefab, int amount, Vector2 scaleRange, float spawnHeight)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
                spawnHeight,
                Random.Range(-spawnAreaSize.y / 2f, spawnAreaSize.y / 2f)
            );

            GameObject spawnedResource = Instantiate(prefab, randomPosition, Quaternion.identity);

            float randomScale = Random.Range(scaleRange.x, scaleRange.y);
            spawnedResource.transform.localScale *= randomScale;
        }
    }
}