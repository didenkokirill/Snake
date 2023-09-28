using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private float spawnDelay;
    [SerializeField] private Vector2 spawnArea;

    [SerializeField] private GameObject applePrefab;

    [SerializeField] private GameObject currentFood;

    private void Update()
    {
        if (currentFood == null)
        {
            SpawnFood(DefinitionPositin());
        }

        spawnArea.x = ViewportHandler.Instance.Width / 2 - 1;
        spawnArea.y = ViewportHandler.Instance.Height / 2 - 1;
    }

    private Vector3 DefinitionPositin()
    {
        Vector3 position = new(
            Mathf.Round(Random.Range(-spawnArea.x, spawnArea.x)),
            Mathf.Round(Random.Range(-spawnArea.y, spawnArea.y)),
            0f);

        return position;
    }

    private void SpawnFood(Vector3 pos)
    {
        GameObject food = Instantiate(applePrefab, pos, Quaternion.identity);

        currentFood = food;
    }
}
