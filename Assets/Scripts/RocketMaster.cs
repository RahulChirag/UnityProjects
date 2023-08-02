using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMaster : MonoBehaviour
{
    public static RocketMaster Instance;

    [System.Serializable]
    public struct S_Objectlist
    {
        public char charecter;
        public RectTransform prefab;
    }

    public List<S_Objectlist> objects;
    public List<string> spellings;
    public GameObject framePrefab;
    public GameObject answerContainer;
    public RectTransform playerArea;

    public float spawnDelay = 1.0f; // Delay between spawns in seconds  

    private Coroutine spawnCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine to spawn objects with a delay
        spawnCoroutine = StartCoroutine(SpawnRandomObjectsWithDelay());        
    }

    // Update is called once per frame
    void Update()
    {        
        
    }

    private IEnumerator SpawnRandomObjectsWithDelay()
    {
        while (objects.Count > 0)
        {
            // Get a random object from the list
            int randomIndex = Random.Range(0, objects.Count);
            S_Objectlist obj = objects[randomIndex];

            // Calculate random y position within the player area bounds
            Vector2 minBoundary = (Vector2)playerArea.rect.min + new Vector2(playerArea.position.x, playerArea.position.y);
            Vector2 maxBoundary = (Vector2)playerArea.rect.max + new Vector2(playerArea.position.x, playerArea.position.y);
            float randomY = Random.Range(minBoundary.y, maxBoundary.y);
            float offset = 500f;
            Vector3 spawnPosition = new Vector3(maxBoundary.x + offset, randomY, 0f);

            // Instantiate the object prefab at the calculated position
            RectTransform spawnedObject = Instantiate(obj.prefab, spawnPosition, Quaternion.identity);
            spawnedObject.SetParent(playerArea); // Make sure the object is a child of the player area

            // Wait for the specified delay before spawning the next object
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
