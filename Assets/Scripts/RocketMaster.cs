using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMaster : MonoBehaviour
{
    public static RocketMaster Instance;  
    
    public List<RectTransform> prefab;
    
    public GameObject framePrefab;
    public RectTransform answerContainer;
    public RectTransform playerArea;
    public List<string> toSpell;
    public List<char> charList;

    public float spawnDelay = 3.0f; // Delay between spawns in seconds  

    private Coroutine spawnCoroutine;
    private int index = 0;

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
        SeperateSpelling(index);
        spawnCoroutine = StartCoroutine(SpawnRandomObjectsWithDelay());
        
    }

    // Update is called once per frame
    void Update()
    {        
        
    }

    private IEnumerator SpawnRandomObjectsWithDelay()
    {
        // Create a weighted list to include both prefabs and characters from charList
        List<object> weightedList = new List<object>();

        // Add prefabs to the weighted list with a weight of 1
        foreach (RectTransform prefabToCheck in prefab)
        {
            weightedList.Add(prefabToCheck);
        }

        // Add characters from charList to the weighted list with a weight based on their occurrences
        foreach (char character in charList)
        {
            int weight = 0;
            foreach (RectTransform prefabToCheck in prefab)
            {
                if (prefabToCheck.GetComponent<ObjectScript>().name == character.ToString())
                {
                    weight++;
                }
            }

            for (int i = 0; i < weight; i++)
            {
                weightedList.Add(character);
            }
        }

        while (prefab.Count > 0)
        {
            // Calculate random y position within the player area bounds
            Vector2 minBoundary = (Vector2)playerArea.rect.min + new Vector2(playerArea.position.x, playerArea.position.y);
            Vector2 maxBoundary = (Vector2)playerArea.rect.max + new Vector2(playerArea.position.x, playerArea.position.y);
            float randomY = Random.Range(minBoundary.y, maxBoundary.y);
            float offset = 500f;
            Vector3 spawnPosition = new Vector3(maxBoundary.x + offset, randomY, 0f);

            // Get a random object from the weighted list
            int randomIndex = Random.Range(0, weightedList.Count);
            object spawnObject = weightedList[randomIndex];

            // If the object is a prefab, spawn it
            if (spawnObject is RectTransform prefabToSpawn)
            {
                // Instantiate the object prefab at the calculated position
                RectTransform spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
                spawnedObject.SetParent(playerArea); // Make sure the object is a child of the player area
            }

            // Wait for the specified delay before spawning the next object
            yield return new WaitForSeconds(spawnDelay);
        }
    }



    void SeperateSpelling(int index)
    {
        foreach(char character in toSpell[index])
        {
            charList.Add(character);            
            GameObject frame = Instantiate(framePrefab);
            frame.transform.SetParent(answerContainer);
            frame.transform.GetChild(0).localScale = Vector2.zero;
        }
    }
}
