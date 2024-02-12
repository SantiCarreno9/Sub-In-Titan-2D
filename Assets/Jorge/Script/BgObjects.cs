using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgObjects : MonoBehaviour
{
    [SerializeField] private GameObject[] objectPrefabs;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float spawnDistance = 10f;

    private GameObject[] spawnedObjects;

    private void Start()
    {
        // Initialize the object pool
        spawnedObjects = new GameObject[objectPrefabs.Length];
        for (int i = 0; i < objectPrefabs.Length; i++)
        {
            spawnedObjects[i] = Instantiate(objectPrefabs[i]);
            spawnedObjects[i].SetActive(false);
        }
    }

    private void Update()
    {
        // Check the distance between the player and each spawned object
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj.activeSelf)
            {
                float distanceToPlayer = Vector3.Distance(obj.transform.position, playerTransform.position);
                if (distanceToPlayer > spawnDistance)
                {
                    // If the object is too far away, deactivate it
                    obj.SetActive(false);
                }
            }
            else
            {
                // If the object is not active, try to spawn it
                if (Random.Range(0f, 1f) < 0.1f) // Adjust the probability as needed
                {
                    Vector3 randomOffset = Random.insideUnitSphere * spawnDistance;
                    randomOffset.y = 0f; // Ensure objects spawn at the same height as the player
                    obj.transform.position = playerTransform.position + randomOffset;
                    obj.SetActive(true);
                }
            }
        }
    }
}
