using System.Collections;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private float spawnDelay;
    [SerializeField] private Vector2 spawnArea;

    [SerializeField] private GameObject apple;

    [SerializeField] private GameObject currentFood;

    private void Update()
    {
        if (currentFood != null)
        {
            return;
        }

        SpawnFood(DefinitionPositin());
    }

    private Vector3 DefinitionPositin()
    {
        Vector3 position = new Vector3(
            Mathf.Round(Random.Range(-spawnArea.x, spawnArea.x)),
            Mathf.Round(Random.Range(-spawnArea.y, spawnArea.y)),
            0f);

        return (position);
    }

    private void SpawnFood(Vector3 pos)
    {
        GameObject food = Instantiate(apple, pos, Quaternion.identity);

        currentFood = food;
    }
}
