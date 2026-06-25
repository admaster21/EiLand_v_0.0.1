using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public GameObject treePrefab;
    public GameObject rockPrefab;
    public GameObject fiberPrefab;

    public int treeAmount = 0;
    public int rockAmount = 0;
    public int fiberAmount = 0;

    public Vector2 spawnAreaSize = new Vector2(60f, 60f);
    public float treeGroundOffset = 0f;
    public float rockGroundOffset = 0f;
    public float fiberGroundOffset = 0f;
    public Vector2 treeScaleRange = new Vector2(0.8f, 1.3f);
    public Vector2 rockScaleRange = new Vector2(0.8f, 1.4f);
    public Vector2 rockWidthRange = new Vector2(0.7f, 1.4f);
    public Vector2 rockHeightRange = new Vector2(0.5f, 1.1f);
    public Vector2 rockDepthRange = new Vector2(0.7f, 1.4f);
    public Vector2 fiberScaleRange = new Vector2(0.8f, 1.2f);

    void Start()
    {
        SpawnResource(treePrefab, treeAmount, treeScaleRange, treeGroundOffset);
        SpawnShapedResource(rockPrefab, rockAmount,rockWidthRange,rockHeightRange,
            rockDepthRange, rockGroundOffset);
        SpawnResource(fiberPrefab, fiberAmount, fiberScaleRange, fiberGroundOffset);
    }

    void SpawnResource(GameObject prefab, int amount, Vector2 scaleRange, float groundOffset)
    {
        if (prefab == null)
        {
            return;
        }

        for (int i = 0; i < amount; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
                0f,
                Random.Range(-spawnAreaSize.y / 2f, spawnAreaSize.y / 2f)
            );

            Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            GameObject spawnedResource = Instantiate(prefab, randomPosition, randomRotation);

            float randomScale = Random.Range(scaleRange.x, scaleRange.y);
            spawnedResource.transform.localScale *= randomScale;

            MoveBottomToGround(spawnedResource, groundOffset);
        }
    }

    void MoveBottomToGround(GameObject spawnedResource, float groundOffset)
    {
        Renderer[] renderers = spawnedResource.GetComponentsInChildren<Renderer>();

        if (renderers.Length == 0)
        {
            return;
        }

        Bounds bounds = renderers[0].bounds;

        for (int i = 1; i < renderers.Length; i++)
        {
            bounds.Encapsulate(renderers[i].bounds);
        }

        float heightOffset = groundOffset - bounds.min.y;
        spawnedResource.transform.position += Vector3.up * heightOffset;
    }

    void SpawnShapedResource(GameObject prefab, int amount, Vector2 widthRange,
        Vector2 heightRange, Vector2 depthRange, float groundOffset)
    {
        if(prefab == null)
        {
            return;
        }

        for (int i = 0; i < amount; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2),
                0f, Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2));

            Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

            GameObject spawnedResoure = Instantiate(prefab, randomPosition, randomRotation);

            Vector3 randomShape = new Vector3(
            Random.Range(widthRange.x, widthRange.y),
            Random.Range(heightRange.x, heightRange.y),
            Random.Range(depthRange.x, depthRange.y)
            );

            spawnedResoure.transform.localScale = Vector3.Scale(spawnedResoure.transform.localScale, randomShape);

            MoveBottomToGround(spawnedResoure, groundOffset);
        }
    }
}
